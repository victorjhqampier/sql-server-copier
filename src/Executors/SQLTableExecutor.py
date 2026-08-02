from __future__ import annotations

from typing import Any

from src.library.LoggerLibrary import LoggerLibrary
from src.library.SqlOriginHandler import SqlOriginHandler, parse_table_name
from src.library.SqlServerConnector import ConnectionString, SqlServerConnector
from src.library.SqlTargetHandler import SqlTargetHandler


class SQLTableExecutor:
    """Copia la estructura (DDL) de las tablas origen hacia el SQL Server destino.

    Para cada tabla:
      1. lee la metadata de columnas, tipos, identity y clave primaria desde el origen
      2. crea el schema en el destino si no existe
      3. genera y ejecuta el CREATE TABLE en el destino
    """

    def __init__(
        self,
        source: ConnectionString,
        target: ConnectionString,
        tables: list[dict[str, Any]],
        drop_if_exists: bool = False,
    ) -> None:
        if not tables:
            raise ValueError("tables no puede ser una lista vacia")

        self._tables = tables
        self._drop_if_exists = drop_if_exists
        self._logger = LoggerLibrary.set_logger().getChild(self.__class__.__name__)
        self._connector = SqlServerConnector(logger=self._logger)
        self._source = SqlOriginHandler(source, connector=self._connector, logger=self._logger)
        self._target = SqlTargetHandler(target, connector=self._connector, logger=self._logger)

    def run(self) -> None:
        self._logger.info(f"Iniciando copia de estructura para {len(self._tables)} tabla(s)")
        try:
            for table_cfg in self._tables:
                try:
                    self._create_table(table_cfg)
                except Exception:
                    self._logger.error(f"Error copiando estructura para {table_cfg}", exc_info=True)
        finally:
            self._connector.close_all()

    def _create_table(self, table_cfg: dict[str, Any]) -> None:
        schema, table = parse_table_name(table_cfg["table"])
        target_schema = table_cfg.get("target_schema", schema)

        if self._drop_if_exists:
            self._target.drop_table_if_exists(target_schema, table)
        elif self._target.table_exists(target_schema, table):
            self._logger.warning(
                f"Tabla {target_schema}.{table} ya existe en destino. Se omite (usa --drop-if-exists para recrearla)"
            )
            return

        columns = self._source.get_table_metadata(schema, table)
        if not columns:
            raise ValueError(f"No se encontro metadata en el origen para {schema}.{table}")

        primary_key = self._source.get_primary_key(schema, table)
        primary_key_name = self._source.get_primary_key_name(schema, table)
        self._target.ensure_schema(target_schema)

        create_sql = self._target.build_create_table(target_schema, table, columns, primary_key, primary_key_name)
        self._target.execute(create_sql)
        self._logger.info(
            f"Estructura creada en destino: {target_schema}.{table} ({len(columns)} columnas, PK={primary_key or 'ninguna'})"
        )

        for index in self._source.get_indexes(schema, table):
            self._target.try_create_index(target_schema, table, index)

        for property_metadata in self._source.get_extended_properties(schema, table):
            self._target.try_add_extended_property(target_schema, table, property_metadata)
