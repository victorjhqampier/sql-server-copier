from __future__ import annotations

import json
from pathlib import Path
from typing import Any, Sequence

from src.library.SqlOriginHandler import parse_table_name


class CreateDebeziumMapHandler:
    """Actualiza la lista de tablas capturadas por Debezium."""

    def __init__(self, config_path: Path | str) -> None:
        self._config_path = Path(config_path)

    @staticmethod
    def _debezium_table_name(table_name: str) -> str:
        schema, table = parse_table_name(table_name)
        return f"{schema}\\.{table}"

    def build_table_include_list(self, tables: Sequence[dict[str, Any]]) -> str:
        if not tables:
            raise ValueError("tables no puede estar vacio")

        return ",".join(self._debezium_table_name(table_cfg["table"]) for table_cfg in tables)

    def update_table_include_list(self, tables: Sequence[dict[str, Any]]) -> Path:
        if not self._config_path.exists() or not self._config_path.is_file():
            raise FileNotFoundError(f"No existe config Debezium: {self._config_path}")

        configuration = json.loads(self._config_path.read_text(encoding="utf-8"))
        configuration["table.include.list"] = self.build_table_include_list(tables)
        self._config_path.write_text(f"{json.dumps(configuration, indent=2)}\n", encoding="utf-8")
        return self._config_path
