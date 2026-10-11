from __future__ import annotations

from dataclasses import dataclass
from typing import Any, Sequence

from src.library.LoggerLibrary import LoggerLibrary
from src.library.ScylladbConnector import ScylladbConnectionString, ScylladbConnector
from src.library.cql_builders import build_create_table_cql


@dataclass(frozen=True)
class ScylladbColumnDefinition:
    """Columna ya traducida para los destinos ScyllaDB y C#."""

    name: str
    cql_type: str
    nullable: bool = False
    source_name: str | None = None
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
        return build_create_table_cql(keyspace, table, columns, partition_key, clustering_key)

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
