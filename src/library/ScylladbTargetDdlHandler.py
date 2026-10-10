from __future__ import annotations

from dataclasses import dataclass
from typing import Any, Sequence

from src.library.LoggerLibrary import LoggerLibrary
from src.library.ScylladbConnector import ScylladbConnectionString, ScylladbConnector


@dataclass(frozen=True)
class ScylladbColumnDefinition:
    """Columna ya traducida para los destinos ScyllaDB y C#."""

    name: str
    cql_type: str
    nullable: bool = False
    cs_property_name: str | None = None
    cs_type: str | None = None


class ScylladbTargetDdlHandler:
    """Crea tablas en ScyllaDB usando una sesion administrada externamente."""

    def __init__(
        self,
        connection_string: ScylladbConnectionString,
        connector: ScylladbConnector,
        logger: Any = None,
    ) -> None:
        self._connection_string = connection_string
        self._connector = connector
        self._logger = logger or LoggerLibrary.set_logger().getChild(self.__class__.__name__)

    @staticmethod
    def _quote_identifier(value: str) -> str:
        if not value or not str(value).strip():
            raise ValueError("Identificador CQL no puede estar vacio")
        return f'"{str(value).replace('"', '""')}"'

    @staticmethod
    def _validate_cql_type(value: str) -> str:
        cql_type = str(value).strip()
        if not cql_type:
            raise ValueError("El tipo CQL no puede estar vacio")
        if ";" in cql_type:
            raise ValueError("El tipo CQL no puede contener punto y coma")
        return cql_type

    def execute(self, cql: str) -> None:
        """Ejecuta una sentencia CQL usando la sesion administrada por el conector."""
        self._connector.get_session(self._connection_string).execute(cql)

    def build_create_table(
        self,
        keyspace: str,
        table: str,
        columns: Sequence[ScylladbColumnDefinition],
        partition_key: Sequence[str],
        clustering_key: Sequence[str] = (),
    ) -> str:
        if not columns:
            raise ValueError("columns no puede estar vacio")
        if not partition_key:
            raise ValueError("partition_key no puede estar vacio")

        column_names = {column.name for column in columns}
        primary_key = [*partition_key, *clustering_key]
        if len(primary_key) != len(set(primary_key)):
            raise ValueError("partition_key y clustering_key no pueden repetir columnas")
        missing_columns = set(primary_key) - column_names
        if missing_columns:
            raise ValueError(f"Las columnas de PRIMARY KEY no existen: {', '.join(sorted(missing_columns))}")

        column_definitions = [
            f"{self._quote_identifier(column.name)} {self._validate_cql_type(column.cql_type)}" for column in columns
        ]
        quoted_partition_key = ", ".join(self._quote_identifier(column) for column in partition_key)
        if len(partition_key) > 1:
            quoted_partition_key = f"({quoted_partition_key})"
        quoted_clustering_key = ", ".join(self._quote_identifier(column) for column in clustering_key)
        primary_key_definition = f"PRIMARY KEY ({quoted_partition_key}"
        if quoted_clustering_key:
            primary_key_definition += f", {quoted_clustering_key}"
        primary_key_definition += ")"

        definitions = ",\n    ".join([*column_definitions, primary_key_definition])
        return (
            f"CREATE TABLE IF NOT EXISTS {self._quote_identifier(keyspace)}.{self._quote_identifier(table)} (\n"
            f"    {definitions}\n"
            ");"
        )

    def create_table(
        self,
        keyspace: str,
        table: str,
        columns: Sequence[ScylladbColumnDefinition],
        partition_key: Sequence[str],
        clustering_key: Sequence[str] = (),
    ) -> None:
        cql = self.build_create_table(keyspace, table, columns, partition_key, clustering_key)
        self.execute(cql)
        self._logger.info(f"Tabla ScyllaDB creada o existente: {keyspace}.{table}")
