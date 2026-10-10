from __future__ import annotations

import json
from pathlib import Path
from typing import Any, Sequence

from src.library.SqlOriginHandler import parse_table_name


class CreateDebeziumMapHandler:
    """Actualiza la lista de tablas capturadas por Debezium y genera el script CDC."""

    def __init__(
        self,
        config_path: Path | str,
        cdc_sql_path: Path | str | None = None,
    ) -> None:
        self._config_path = Path(config_path)
        self._cdc_sql_path = Path(cdc_sql_path) if cdc_sql_path else None

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

    def build_enable_cdc_sql(self, tables: Sequence[dict[str, Any]]) -> str:
        if not tables:
            raise ValueError("tables no puede estar vacio")
        if self._cdc_sql_path is None:
            raise ValueError("cdc_sql_path no configurado")

        lines = [
            "-- Generado por CreateDebeziumMapHandler.",
            "-- Habilita CDC en SQL Server para las tablas procesadas.",
            "",
            "USE [$(DB_NAME)];",
            "GO",
            "",
            "IF (SELECT is_cdc_enabled FROM sys.databases WHERE name = DB_NAME()) = 0",
            "    EXEC sys.sp_cdc_enable_db;",
            "GO",
            "",
        ]

        for table_cfg in tables:
            schema, table = parse_table_name(table_cfg["table"])
            full_name = f"{schema}.{table}"
            lines.append(
                f"IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'{full_name}'))"
            )
            lines.append(
                f"    EXEC sys.sp_cdc_enable_table "
                f"@source_schema = N'{schema}', "
                f"@source_name = N'{table}', "
                f"@role_name = NULL, "
                f"@supports_net_changes = 0;"
            )
            lines.append("GO")
            lines.append("")

        return "\n".join(lines)

    def write_enable_cdc_sql(self, tables: Sequence[dict[str, Any]]) -> Path:
        if self._cdc_sql_path is None:
            raise ValueError("cdc_sql_path no configurado")
        sql = self.build_enable_cdc_sql(tables)
        self._cdc_sql_path.parent.mkdir(parents=True, exist_ok=True)
        self._cdc_sql_path.write_text(sql, encoding="utf-8")
        return self._cdc_sql_path

    def sync(self, tables: Sequence[dict[str, Any]]) -> tuple[Path, Path | None]:
        debezium_path = self.update_table_include_list(tables)
        cdc_sql_path = self.write_enable_cdc_sql(tables) if self._cdc_sql_path else None
        return debezium_path, cdc_sql_path
