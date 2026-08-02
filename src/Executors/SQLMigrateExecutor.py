from __future__ import annotations

from typing import Any

from src.library.LoggerLibrary import LoggerLibrary
from src.library.SqlOriginHandler import SqlOriginHandler, parse_table_name
from src.library.SqlServerConnector import ConnectionString, SqlServerConnector
from src.library.SqlTargetHandler import SqlTargetHandler


class SQLMigrateExecutor:
    """Migra datos (DML) desde el SQL Server origen hacia el SQL Server destino.

    La lectura usa un generador que entrega lotes de `batch_size` registros
    (default 1000). Cada lote se inserta en el destino y se valida el conteo
    final entre origen y destino.
    """

    def __init__(
        self,
        source: ConnectionString,
        target: ConnectionString,
        tables: list[dict[str, Any]],
        batch_size: int = 1000,
    ) -> None:
        if not tables:
            raise ValueError("tables no puede ser una lista vacia")
        if batch_size <= 0:
            raise ValueError("batch_size debe ser mayor a 0")

        self._tables = tables
        self._batch_size = batch_size
        self._logger = LoggerLibrary.set_logger().getChild(self.__class__.__name__)
        self._connector = SqlServerConnector(logger=self._logger)
        self._source = SqlOriginHandler(source, connector=self._connector, logger=self._logger)
        self._target = SqlTargetHandler(target, connector=self._connector, logger=self._logger)

    def run(self) -> None:
        self._logger.info(
            f"Iniciando migracion de datos para {len(self._tables)} tabla(s) con lotes de {self._batch_size}"
        )
        try:
            for table_cfg in self._tables:
                try:
                    self._migrate_table(table_cfg)
                except Exception:
                    self._logger.error(f"Error copiando datos para {table_cfg}", exc_info=True)
            self._create_foreign_keys()
        finally:
            self._connector.close_all()

    def _migrate_table(self, table_cfg: dict[str, Any]) -> None:
        schema, table = parse_table_name(table_cfg["table"])
        target_schema = table_cfg.get("target_schema", schema)

        if not self._target.table_exists(target_schema, table):
            raise ValueError(
                f"La tabla {target_schema}.{table} no existe en el destino. Ejecuta primero SQLTableExecutor"
            )

        columns = self._source.get_column_names(schema, table)
        target_count = self._target.count(target_schema, table)
        if target_count > 0:
            self._logger.warning(
                f"Tabla {target_schema}.{table} ya tiene {target_count} registros en destino. Se omite para evitar duplicados"
            )
            return

        copy_rows = table_cfg.get("copy_rows", 17000)
        order_by = self._source.resolve_copy_order_column(schema, table, table_cfg.get("order_by"))
        source_count = self._source.count(schema, table)
        self._logger.info(
            f"Copiando {schema}.{table} -> {target_schema}.{table}: origen={source_count}, columnas={len(columns)}, copy_rows={copy_rows}"
        )

        if source_count == 0:
            self._logger.info(f"Tabla {schema}.{table} sin registros en origen. Se omite")
            return

        identity_columns = self._source.get_identity_columns(schema, table)
        preserve_identity = bool(identity_columns and identity_columns[0] in columns)
        if preserve_identity:
            self._target.set_identity_insert(target_schema, table, True)

        inserted = 0
        try:
            for batch_index, batch in enumerate(
                self._source.read_batches(
                    schema,
                    table,
                    columns,
                    batch_size=self._batch_size,
                    limit=copy_rows,
                    order_by=order_by,
                ),
                start=1,
            ):
                self._target.insert_rows(target_schema, table, columns, batch)
                inserted += len(batch)
                self._logger.info(
                    f"  {target_schema}.{table} lote {batch_index}: {len(batch)} registros insertados (acumulado {inserted})"
                )
        except Exception:
            self._logger.error(f"Error migrando {schema}.{table}", exc_info=True)
            raise
        finally:
            if preserve_identity:
                self._target.set_identity_insert(target_schema, table, False)

        self._validate_counts(target_schema, table, source_count, inserted, copy_rows)

    def _target_schema_for(self, source_schema: str, source_table: str) -> str:
        for table_cfg in self._tables:
            cfg_schema, cfg_table = parse_table_name(table_cfg["table"])
            if cfg_schema == source_schema and cfg_table == source_table:
                return table_cfg.get("target_schema", cfg_schema)
        return source_schema

    def _create_foreign_keys(self) -> None:
        self._logger.info("Intentando crear foreign keys posteriores a la copia de datos")
        for table_cfg in self._tables:
            schema, table = parse_table_name(table_cfg["table"])
            target_schema = table_cfg.get("target_schema", schema)
            if not self._target.table_exists(target_schema, table):
                continue
            for foreign_key in self._source.get_foreign_keys(schema, table):
                referenced_schema = self._target_schema_for(foreign_key.referenced_schema, foreign_key.referenced_table)
                self._target.try_create_foreign_key(
                    target_schema,
                    table,
                    foreign_key,
                    referenced_schema,
                    foreign_key.referenced_table,
                )

    def _validate_counts(
        self,
        schema: str,
        table: str,
        source_count: int,
        inserted: int,
        copy_rows: int | None,
    ) -> None:
        target_count = self._target.count(schema, table)
        expected = source_count if copy_rows is None else min(source_count, copy_rows)
        if target_count != expected or inserted != expected:
            raise RuntimeError(
                f"Conteos no coinciden para {schema}.{table}: esperado={expected}, insertados={inserted}, destino={target_count}"
            )
        self._logger.info(f"Copia de {schema}.{table} completada: {expected} registros, conteos validados")
