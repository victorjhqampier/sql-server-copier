from __future__ import annotations

import argparse
import os
import sys
from dataclasses import dataclass, field
from pathlib import Path


@dataclass
class EnvironmentVariables:
    extras: dict[str, str] = field(default_factory=dict)

    def get(self, key: str) -> str | None:
        return self.extras.get(self._normalize_key(key))

    @staticmethod
    def _normalize_key(key: str) -> str:
        return key.strip().lstrip("-").replace("-", "_").upper()


class EnvironmentLibrary:
    """Resuelve variables de configuracion desde CLI, variables de entorno o archivo .env."""

    def __init__(self, argv: list[str] | None = None) -> None:
        self._argv = argv

    def _find_dotenv_path(self) -> Path | None:
        candidates = [
            Path.cwd() / ".env",
            Path(__file__).resolve().parents[2] / ".env",
        ]
        for candidate in candidates:
            if candidate.exists() and candidate.is_file():
                return candidate
        return None

    def _read_dotenv(self) -> dict[str, str]:
        values: dict[str, str] = {}
        env_path = self._find_dotenv_path()
        if env_path is None:
            return values

        for line in env_path.read_text(encoding="utf-8").splitlines():
            stripped = line.strip()
            if not stripped or stripped.startswith("#") or "=" not in stripped:
                continue
            key, raw_value = stripped.split("=", 1)
            values[EnvironmentVariables._normalize_key(key)] = raw_value.strip().strip('"').strip("'")

        return values

    def _parse_cli_values(self, keys: list[str]) -> dict[str, str | None]:
        parser = argparse.ArgumentParser(description="Variables de configuracion del migrador")
        for key in keys:
            parser.add_argument(
                f"--{key}",
                f"--{key.lower()}",
                dest=key,
                default=None,
                required=False,
                help=f"Variable {key}",
            )

        parsed_args, _ = parser.parse_known_args(self._argv if self._argv is not None else sys.argv[1:])
        return {key: getattr(parsed_args, key, None) for key in keys}

    def get(self, *args: str, required: bool = True) -> EnvironmentVariables:
        keys = list(dict.fromkeys(args))
        dotenv_values = self._read_dotenv()
        cli_values = self._parse_cli_values(keys)

        extras: dict[str, str] = {}
        missing: list[str] = []
        for key in keys:
            value = cli_values.get(key) or os.getenv(key) or dotenv_values.get(key)
            if value is None or str(value).strip() == "":
                missing.append(key)
                continue
            extras[key] = str(value).strip()

        if required and missing:
            raise ValueError(f"Variables requeridas no encontradas o vacias: {', '.join(missing)}")

        return EnvironmentVariables(extras=extras)
