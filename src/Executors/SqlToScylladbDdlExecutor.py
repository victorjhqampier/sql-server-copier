from __future__ import annotations

import re
import unicodedata
from pathlib import Path
from typing import Any, Sequence

from src.config.scylladb.tables_scylla import tables_scylladb
from src.library.CreateCsFileHandler import CreateCsFileHandler
from src.library.CreateCqlFileHandler import CreateCqlFileHandler
from src.library.CreateDebeziumMapHandler import CreateDebeziumMapHandler
from src.library.CreateZincMapHandler import CreateZincMapHandler
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
_KAFKA_TOPIC_PREFIX_KEY = "KAFKA_TOPIC_PREFIX"
_CDC_TOPIC_PREFIX_DEFAULT = "arify"


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
        "geography": "text",
        "geometry": "text",
        "image": "blob",
        "int": "int",
        "money": "decimal",
        "nchar": "text",
        "ntext": "text",
        "numeric": "decimal",
        "nvarchar": "text",
        "real": "float",
        "smallint": "smallint",
        "smalldatetime": "timestamp",
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
        zinc_output_dir: Path | str = Path(__file__).resolve().parent / "outputApacheZinc",
        debezium_config_path: Path | str = Path(__file__).resolve().parent
        / "outputDebezium"
        / "debezium-sqlserver.json",
        cdc_sql_path: Path | str = Path(__file__).resolve().parent / "outputDebezium" / "enable-cdc.sql",
        namespace: str = "ScylladbInfrastructure.Collections.Tables",
        topic_prefix: str = _CDC_TOPIC_PREFIX_DEFAULT,
    ) -> None:
        if not tables:
            raise ValueError("tables no puede ser una lista vacia")
        if not scylladb.keyspace:
            raise ValueError("SCYLLADB_KEYSPACE es requerido para crear tablas")

        # -- Conserva la configuracion de las tablas que se procesaran en orden.
        self._tables = tables
        self._scylladb = scylladb
        self._logger = LoggerLibrary.set_logger().getChild(self.__class__.__name__)

        # -- Abre y reutiliza conexiones hacia el SQL Server que aporta la metadata.
        self._sql_connector = SqlServerConnector(logger=self._logger)

        # -- Abre y reutiliza sesiones hacia ScyllaDB para ejecutar el DDL.
        self._scylladb_connector = ScylladbConnector(logger=self._logger)

        # -- Lee columnas y claves desde SQL Server, sin modificar el origen.
        self._source = SqlOriginHandler(sqlserver, connector=self._sql_connector, logger=self._logger)

        # -- Materializa tablas ScyllaDB usando la metadata ya traducida.
        self._target = ScylladbTargetDdlHandler(scylladb, connector=self._scylladb_connector, logger=self._logger)

        # -- Genera el modelo C# que representa cada tabla ScyllaDB.
        self._cs_files = CreateCsFileHandler(output_dir, namespace)

        # -- Conserva el topic prefix para Debezium y Zinc.
        self._topic_prefix = topic_prefix

        # -- Genera el archivo Kafka Connect/Zinc que mapea topics hacia ScyllaDB.
        self._zinc_maps = CreateZincMapHandler(zinc_output_dir, topic_prefix=topic_prefix)

        # -- Conserva el DDL CQL para aplicar o revisar el esquema fuera del executor.
        self._cql_files = CreateCqlFileHandler(zinc_output_dir)

        # -- Sincroniza Debezium con la misma lista de tablas usadas por este flujo.
        self._debezium_map = CreateDebeziumMapHandler(debezium_config_path, cdc_sql_path, topic_prefix=topic_prefix)

    def run(self) -> None:
        self._logger.info(f"Iniciando generacion ScyllaDB para {len(self._tables)} tabla(s)")
        try:
            # -- Sincroniza Debezium (JSON + SQL CDC) antes de crear DDL/C#/Zinc.
            debezium_path, cdc_sql_path = self._debezium_map.sync(self._tables)
            self._logger.info(f"Config Debezium actualizada: {debezium_path}")
            if cdc_sql_path:
                self._logger.info(f"Script CDC generado: {cdc_sql_path}")

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
        value = SqlToScylladbDdlExecutor._normalize_identifier(value)
        words = re.sub(r"([a-z0-9])([A-Z])", r"\1_\2", value)
        words = re.sub(r"([A-Z]+)([A-Z][a-z])", r"\1_\2", words)
        return words.lower()

    @staticmethod
    def _normalize_identifier(value: str) -> str:
        """Convierte caracteres acentuados a ASCII para los identificadores de salida."""
        normalized = unicodedata.normalize("NFKD", value).encode("ascii", "ignore").decode("ascii")
        if not normalized:
            raise ValueError(f"Identificador sin caracteres ASCII utilizables: {value}")
        return normalized

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
                name=cls._normalize_identifier(column.name),
                cql_type=cls._cql_type_for(column),
                nullable=column.nullable,
                source_name=column.name,
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
        # -- Resuelve la tabla SQL Server y el nombre destino ScyllaDB.
        source_schema, source_table = parse_table_name(table_cfg["table"])
        keyspace = table_cfg.get("keyspace", self._scylladb.keyspace)
        target_table = table_cfg.get("target_table", self._snake_case(source_table))
        if not isinstance(keyspace, str) or not keyspace.strip():
            raise ValueError(f"keyspace invalido para {source_schema}.{source_table}")
        if not isinstance(target_table, str) or not target_table.strip():
            raise ValueError(f"target_table invalida para {source_schema}.{source_table}")

        # -- Lee columnas, tipos y nulabilidad desde SQL Server.
        columns = self._source.get_table_metadata(source_schema, source_table)
        if not columns:
            raise ValueError(f"No se encontro metadata en el origen para {source_schema}.{source_table}")

        # -- Usa la PK SQL Server como partition key si la configuracion no define otra.
        source_primary_key = self._source.get_primary_key(source_schema, source_table)
        partition_key = self._key_columns(table_cfg, "partition_key", source_primary_key)
        if not partition_key:
            raise ValueError(
                f"{source_schema}.{source_table} no tiene PK SQL Server; agrega "
                f"{{'table': '{source_schema}.{source_table}', 'partition_key': ['columna']}} "
                "en src/config/scylladb/tables_scylla.py"
            )
        clustering_key = self._key_columns(table_cfg, "clustering_key")
        # -- Traduce tipos SQL Server a una definicion compartida por los tres destinos.
        definitions = self._translate_columns(columns)
        source_to_target_column = {column.source_name: column.name for column in definitions}
        if len(source_to_target_column) != len(definitions):
            raise ValueError(f"{source_schema}.{source_table} contiene columnas repetidas")
        if len(set(source_to_target_column.values())) != len(definitions):
            raise ValueError(f"{source_schema}.{source_table} contiene columnas que coinciden al remover acentos")
        try:
            partition_key = tuple(source_to_target_column[column] for column in partition_key)
            clustering_key = tuple(source_to_target_column[column] for column in clustering_key)
        except KeyError as exc:
            raise ValueError(f"La columna de llave {exc.args[0]} no existe en {source_schema}.{source_table}") from exc

        # -- Crea la tabla fisica en ScyllaDB.
        self._target.create_table(keyspace.strip(), target_table.strip(), definitions, partition_key, clustering_key)

        # -- Genera la clase C# para acceder a la tabla desde .NET.
        output_path = self._cs_files.create_table(
            keyspace.strip(), target_table.strip(), definitions, partition_key, clustering_key
        )
        self._logger.info(f"Modelo C# generado: {output_path}")

        # -- Usa el mismo patron de topic que Debezium genera: topic_prefix.schema.TablaSQLServer.
        cdc_topic = f"{self._topic_prefix}.{source_schema}.{source_table}"
        zinc_output_path = self._zinc_maps.create_table(
            keyspace.strip(),
            target_table.strip(),
            definitions,
            partition_key,
            clustering_key,
            topic=cdc_topic,
        )
        self._logger.info(f"Mapping Zinc generado: {zinc_output_path}")

        cql_output_path = self._cql_files.create_table(
            keyspace.strip(), target_table.strip(), definitions, partition_key, clustering_key
        )
        self._logger.info(f"CQL generado: {cql_output_path}")


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
    topic_prefix_env = environment.get(_KAFKA_TOPIC_PREFIX_KEY, required=False)
    topic_prefix = topic_prefix_env.extras.get(_KAFKA_TOPIC_PREFIX_KEY, _CDC_TOPIC_PREFIX_DEFAULT)
    SqlToScylladbDdlExecutor(sqlserver, scylladb, tables_scylladb, topic_prefix=topic_prefix).run()


if __name__ == "__main__":
    main()
