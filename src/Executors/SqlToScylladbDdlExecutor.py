from __future__ import annotations

import re
from pathlib import Path
from typing import Any, Sequence

from src.config.scylladb.tables_scylla import tables_scylladb
from src.library.CreateCsFileHandler import CreateCsFileHandler
from src.library.EnvironmentLibrary import EnvironmentLibrary, EnvironmentVariables
from src.library.LoggerLibrary import LoggerLibrary
from src.library.ScylladbConnector import ScylladbConnectionString, ScylladbConnector
from src.library.ScylladbTargetDdlHandler import ScylladbColumnDefinition, ScylladbTargetDdlHandler
from src.library.SqlOriginHandler import ColumnMetadata, SqlOriginHandler, parse_table_name
from src.library.SqlServerConnector import ConnectionString, SqlServerConnector

_TARGET_SQLSERVER_KEYS = (
    "TARGET_SQLSERVER_HOST",
    "TARGET_SQLSERVER_USER",
    "TARGET_SQLSERVER_PASSWORD",
    "TARGET_SQLSERVER_DATABASE",
)
_SCYLLADB_REQUIRED_KEYS = ("SCYLLADB_CONTACT_POINTS", "SCYLLADB_KEYSPACE")
_SCYLLADB_OPTIONAL_KEYS = ("SCYLLADB_PORT", "SCYLLADB_USERNAME", "SCYLLADB_PASSWORD")


class SqlToScylladbDdlExecutor:
    """Traduce estructura SQL Server destino a tablas ScyllaDB y modelos C#."""

    _CQL_TYPES = {
        "bigint": "bigint",
        "binary": "blob",
        "bit": "boolean",
        "char": "text",
        "date": "date",
        "datetime": "timestamp",
        "datetime2": "timestamp",
        "datetimeoffset": "timestamp",
        "decimal": "decimal",
        "float": "double",
        "image": "blob",
        "int": "int",
        "money": "decimal",
        "nchar": "text",
        "ntext": "text",
        "numeric": "decimal",
        "nvarchar": "text",
        "real": "float",
        "smallint": "smallint",
        "smallmoney": "decimal",
        "text": "text",
        "time": "time",
        "timestamp": "blob",
        "tinyint": "tinyint",
        "uniqueidentifier": "uuid",
        "varbinary": "blob",
        "varchar": "text",
        "xml": "text",
    }

    def __init__(
        self,
        sqlserver: ConnectionString,
        scylladb: ScylladbConnectionString,
        tables: list[dict[str, Any]],
        output_dir: Path | str = Path(__file__).resolve().parent / "outputCsNet",
        namespace: str = "ScylladbInfrastructure.Collections.Tables",
    ) -> None:
        if not tables:
            raise ValueError("tables no puede ser una lista vacia")
        if not scylladb.keyspace:
            raise ValueError("SCYLLADB_KEYSPACE es requerido para crear tablas")

        self._tables = tables
        self._scylladb = scylladb
        self._logger = LoggerLibrary.set_logger().getChild(self.__class__.__name__)
        self._sql_connector = SqlServerConnector(logger=self._logger)
        self._scylladb_connector = ScylladbConnector(logger=self._logger)
        self._source = SqlOriginHandler(sqlserver, connector=self._sql_connector, logger=self._logger)
        self._target = ScylladbTargetDdlHandler(scylladb, connector=self._scylladb_connector, logger=self._logger)
        self._cs_files = CreateCsFileHandler(output_dir, namespace)

    def run(self) -> None:
        self._logger.info(f"Iniciando generacion ScyllaDB para {len(self._tables)} tabla(s)")
        try:
            for table_cfg in self._tables:
                try:
                    self._create_table(table_cfg)
                except Exception:
                    self._logger.error(f"Error generando estructura ScyllaDB para {table_cfg}", exc_info=True)
        finally:
            self._scylladb_connector.close_all()
            self._sql_connector.close_all()

    @staticmethod
    def _snake_case(value: str) -> str:
        words = re.sub(r"([a-z0-9])([A-Z])", r"\1_\2", value)
        words = re.sub(r"([A-Z]+)([A-Z][a-z])", r"\1_\2", words)
        return words.lower()

    @classmethod
    def _cql_type_for(cls, column: ColumnMetadata) -> str:
        data_type = column.data_type.lower()
        cql_type = cls._CQL_TYPES.get(data_type)
        if cql_type is None:
            raise ValueError(f"Tipo SQL Server no soportado para ScyllaDB: {column.data_type} ({column.name})")
        return cql_type

    @classmethod
    def _translate_columns(cls, columns: Sequence[ColumnMetadata]) -> list[ScylladbColumnDefinition]:
        return [
            ScylladbColumnDefinition(
                name=column.name,
                cql_type=cls._cql_type_for(column),
                nullable=column.nullable,
            )
            for column in columns
        ]

    @staticmethod
    def _key_columns(table_cfg: dict[str, Any], key: str, default: Sequence[str] = ()) -> tuple[str, ...]:
        value = table_cfg.get(key, default)
        if isinstance(value, str):
            values = (value,)
        elif isinstance(value, Sequence):
            values = tuple(value)
        else:
            raise ValueError(f"{key} debe ser una columna o una secuencia de columnas")

        if not all(isinstance(column, str) and column.strip() for column in values):
            raise ValueError(f"{key} solo puede contener nombres de columnas no vacios")
        return tuple(column.strip() for column in values)

    def _create_table(self, table_cfg: dict[str, Any]) -> None:
        source_schema, source_table = parse_table_name(table_cfg["table"])
        keyspace = table_cfg.get("keyspace", self._scylladb.keyspace)
        target_table = table_cfg.get("target_table", self._snake_case(source_table))
        if not isinstance(keyspace, str) or not keyspace.strip():
            raise ValueError(f"keyspace invalido para {source_schema}.{source_table}")
        if not isinstance(target_table, str) or not target_table.strip():
            raise ValueError(f"target_table invalida para {source_schema}.{source_table}")

        columns = self._source.get_table_metadata(source_schema, source_table)
        if not columns:
            raise ValueError(f"No se encontro metadata en el origen para {source_schema}.{source_table}")

        source_primary_key = self._source.get_primary_key(source_schema, source_table)
        partition_key = self._key_columns(table_cfg, "partition_key", source_primary_key)
        if not partition_key:
            raise ValueError(
                f"{source_schema}.{source_table} no tiene PK SQL Server; configura partition_key explicitamente"
            )
        clustering_key = self._key_columns(table_cfg, "clustering_key")
        definitions = self._translate_columns(columns)

        self._target.create_table(keyspace.strip(), target_table.strip(), definitions, partition_key, clustering_key)
        output_path = self._cs_files.create_table(
            keyspace.strip(), target_table.strip(), definitions, partition_key, clustering_key
        )
        self._logger.info(f"Modelo C# generado: {output_path}")


def _target_sqlserver_connection_string(env: EnvironmentVariables) -> ConnectionString:
    return ConnectionString(
        host=env.get("TARGET_SQLSERVER_HOST"),
        user=env.get("TARGET_SQLSERVER_USER"),
        password=env.get("TARGET_SQLSERVER_PASSWORD"),
        database=env.get("TARGET_SQLSERVER_DATABASE"),
    )


def _scylladb_connection_string(
    required: EnvironmentVariables,
    optional: EnvironmentVariables,
) -> ScylladbConnectionString:
    contact_points = tuple(
        point.strip() for point in (required.get("SCYLLADB_CONTACT_POINTS") or "").split(",") if point.strip()
    )
    port_value = optional.get("SCYLLADB_PORT") or "9042"
    try:
        port = int(port_value)
    except ValueError as exc:
        raise ValueError(f"SCYLLADB_PORT debe ser un entero: {port_value}") from exc

    return ScylladbConnectionString(
        contact_points=contact_points,
        port=port,
        username=optional.get("SCYLLADB_USERNAME"),
        password=optional.get("SCYLLADB_PASSWORD"),
        keyspace=required.get("SCYLLADB_KEYSPACE"),
    )


def main() -> None:
    environment = EnvironmentLibrary()
    sqlserver = _target_sqlserver_connection_string(environment.get(*_TARGET_SQLSERVER_KEYS))
    scylladb = _scylladb_connection_string(
        environment.get(*_SCYLLADB_REQUIRED_KEYS),
        environment.get(*_SCYLLADB_OPTIONAL_KEYS, required=False),
    )
    SqlToScylladbDdlExecutor(sqlserver, scylladb, tables_scylladb).run()


if __name__ == "__main__":
    main()
