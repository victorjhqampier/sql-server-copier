from __future__ import annotations

from pathlib import Path
from typing import Sequence

from src.library.ScylladbTargetDdlHandler import ScylladbColumnDefinition
from src.library.cql_builders import build_create_table_cql


class CreateCqlFileHandler:
    """Genera un archivo CQL consolidado a partir de definiciones de tablas ScyllaDB."""

    def __init__(self, output_dir: Path | str) -> None:
        self._output_dir = Path(output_dir)
        self._statements: dict[str, str] = {}

    def build_create_table(
        self,
        keyspace: str,
        table: str,
        columns: Sequence[ScylladbColumnDefinition],
        partition_key: Sequence[str],
        clustering_key: Sequence[str] = (),
    ) -> str:
        """Construye el CQL de una tabla sin escribir archivos."""
        return build_create_table_cql(keyspace, table, columns, partition_key, clustering_key)

    def create_table(
        self,
        keyspace: str,
        table: str,
        columns: Sequence[ScylladbColumnDefinition],
        partition_key: Sequence[str],
        clustering_key: Sequence[str] = (),
    ) -> Path:
        """Actualiza el archivo maestro CQL con la definicion de una tabla."""
        cql = self.build_create_table(keyspace, table, columns, partition_key, clustering_key)
        self._output_dir.mkdir(parents=True, exist_ok=True)

        self._statements[f"{keyspace}.{table}"] = cql
        all_tables_path = self._output_dir / "all_tables.cql"
        all_tables_path.write_text(f"{'\n\n'.join(self._statements.values())}\n", encoding="utf-8")
        return all_tables_path
