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
        skipped = 0
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
                batch_inserted, batch_skipped = self._target.insert_rows_tolerant(
                    target_schema, table, columns, batch
                )
                inserted += batch_inserted
                skipped += batch_skipped
                self._logger.info(
                    f"  {target_schema}.{table} lote {batch_index}: {batch_inserted} insertados, "
                    f"{batch_skipped} omitidos (acumulado: {inserted} insertados, {skipped} omitidos)"
                )
        except Exception:
            self._logger.error(f"Error migrando {schema}.{table}", exc_info=True)
            raise
        finally:
            if preserve_identity:
                self._target.set_identity_insert(target_schema, table, False)

        self._validate_counts(target_schema, table, source_count, inserted, skipped, copy_rows)

    def _validate_counts(
        self,
        schema: str,
        table: str,
        source_count: int,
        inserted: int,
        skipped: int,
        copy_rows: int | None,
    ) -> None:
        target_count = self._target.count(schema, table)
        expected = source_count if copy_rows is None else min(source_count, copy_rows)
        processed = inserted + skipped

        if processed != expected:
            raise RuntimeError(
                f"Conteos no coinciden para {schema}.{table}: esperado={expected}, procesados={processed}"
            )
        if target_count != inserted:
            raise RuntimeError(
                f"Conteo destino no coincide para {schema}.{table}: insertados={inserted}, destino={target_count}"
            )

        if skipped > 0:
            self._logger.warning(
                f"Copia de {schema}.{table} completada con omisiones: "
                f"esperados={expected}, insertados={inserted}, omitidos_fk={skipped}"
            )
        else:
            self._logger.info(f"Copia de {schema}.{table} completada: {inserted} registros, conteos validados")
