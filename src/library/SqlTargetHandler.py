from __future__ import annotations

from typing import Any

from src.library.LoggerLibrary import LoggerLibrary
from src.library.SqlServerConnector import ConnectionString, SqlServerConnector


class SqlTargetHandler:
    """Escritura exclusiva sobre SQL Server destino.

    No abre conexiones. Toda conexion se obtiene desde SqlServerConnector.
    No lee datos desde origen y no decide estrategias de copia.
    """

    def __init__(
        self,
        connection_string: ConnectionString,
        connector: SqlServerConnector,
        logger: Any = None,
    ) -> None:
        self._connection_string = connection_string
        self._connector = connector
        self._logger = logger or LoggerLibrary.set_logger().getChild(self.__class__.__name__)

    def _get_connection(self):
        return self._connector.get_connection(self._connection_string)

    @staticmethod
    def _quote_identifier(value: str) -> str:
        if not value or not str(value).strip():
            raise ValueError("Identificador SQL no puede estar vacio")
        return f"[{str(value).replace(']', ']]')}]"

    def _table_identifier(self, schema: str, table: str) -> str:
        return ".".join([self._quote_identifier(schema), self._quote_identifier(table)])

    def execute(self, sql: str, params: tuple | None = None) -> None:
        connection = self._get_connection()
        cursor = connection.cursor()
        try:
            cursor.execute(sql, params or ())
            connection.commit()
        except Exception:
            connection.rollback()
            raise
        finally:
            cursor.close()

    def _query_all(self, sql: str, params: tuple | None = None) -> list[tuple]:
        cursor = self._get_connection().cursor()
        try:
            cursor.execute(sql, params or ())
            return list(cursor.fetchall())
        finally:
            cursor.close()

    def _query_one(self, sql: str, params: tuple | None = None) -> tuple | None:
        rows = self._query_all(sql, params)
        return rows[0] if rows else None

    def schema_exists(self, schema: str) -> bool:
        row = self._query_one("SELECT 1 FROM sys.schemas WHERE name = %s", (schema,))
        return row is not None

    def table_exists(self, schema: str, table: str) -> bool:
        row = self._query_one(
            "SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = %s AND TABLE_NAME = %s",
            (schema, table),
        )
        return row is not None

    def foreign_key_exists(self, schema: str, table: str, fk_name: str) -> bool:
        """Verifica si una foreign key ya existe en la tabla destino."""
        row = self._query_one(
            "SELECT 1 FROM sys.foreign_keys fk "
            "INNER JOIN sys.tables t ON fk.parent_object_id = t.object_id "
            "INNER JOIN sys.schemas s ON t.schema_id = s.schema_id "
            "WHERE s.name = %s AND t.name = %s AND fk.name = %s",
            (schema, table, fk_name),
        )
        return row is not None

    def ensure_schema(self, schema: str) -> None:
        if self.schema_exists(schema):
            return
        self.execute(f"CREATE SCHEMA {self._quote_identifier(schema)}")
        self._logger.info(f"Schema creado: {schema}")

    def drop_table_if_exists(self, schema: str, table: str) -> None:
        if not self.table_exists(schema, table):
            return
        self.execute(f"DROP TABLE {self._table_identifier(schema, table)}")
        self._logger.info(f"Tabla eliminada: {schema}.{table}")

    def count(self, schema: str, table: str) -> int:
        row = self._query_one(f"SELECT COUNT(*) FROM {self._table_identifier(schema, table)}")
        return int(row[0]) if row else 0

    def _sql_type_for(self, column: Any) -> str:
        data_type = column.data_type.lower()
        if data_type in {"char", "varchar", "nchar", "nvarchar", "binary", "varbinary"}:
            if column.max_length is None or int(column.max_length) == -1:
                length = "MAX"
            else:
                length = str(int(column.max_length))
            return f"{data_type}({length})"
        if data_type in {"decimal", "numeric"}:
            precision = int(column.precision or 18)
            scale = int(column.scale or 0)
            return f"{data_type}({precision},{scale})"
        if data_type in {"datetime2", "datetimeoffset", "time"}:
            if column.scale is not None:
                return f"{data_type}({int(column.scale)})"
            return data_type
        return data_type

    @staticmethod
    def _normalize_default(default: str | int | float) -> str:
        value = str(default).strip()
        if value.startswith("(") and value.endswith(")"):
            value = value[1:-1]
        return value

    @staticmethod
    def _is_identity_compatible(column: Any) -> bool:
        """IDENTITY solo es valido en tipos numericos enteros o decimal/numeric con scale 0."""
        data_type = column.data_type.lower()
        if data_type in {"int", "bigint", "smallint", "tinyint"}:
            return True
        if data_type in {"decimal", "numeric"}:
            return column.scale is None or int(column.scale) == 0
        return False

    def _column_definition(self, column: Any, *, is_identity: bool = False) -> str:
        parts = [f"{self._quote_identifier(column.name)} {self._sql_type_for(column)}"]
        if is_identity:
            parts.append("IDENTITY(1,1)")
        if not column.nullable:
            parts.append("NOT NULL")
        if column.default:
            parts.append(f"DEFAULT {self._normalize_default(column.default)}")
        return " ".join(parts)

    def _find_identity_column(self, columns: list[Any]) -> str | None:
        """Encuentra la primera columna IDENTITY valida. Solo puede haber una por tabla."""
        for column in columns:
            if column.is_identity and self._is_identity_compatible(column):
                return column.name
        return None

    def build_create_table(
        self,
        schema: str,
        table: str,
        columns: list[Any],
        primary_key: list[str],
        primary_key_name: str | None = None,
    ) -> str:
        if not columns:
            raise ValueError(f"No hay columnas para construir CREATE TABLE de {schema}.{table}")

        identity_column = self._find_identity_column(columns)
        column_defs = ",\n    ".join(
            self._column_definition(column, is_identity=(column.name == identity_column))
            for column in columns
        )
        primary_key_clause = ""
        if primary_key:
            pk_columns = ", ".join(self._quote_identifier(column) for column in primary_key)
            constraint_name = primary_key_name or f"PK_{table}"
            primary_key_clause = (
                f",\n    CONSTRAINT {self._quote_identifier(constraint_name)} PRIMARY KEY ({pk_columns})"
            )

        return f"CREATE TABLE {self._table_identifier(schema, table)} (\n    {column_defs}{primary_key_clause}\n);"

    def create_index(self, schema: str, table: str, index: Any) -> None:
        unique = "UNIQUE " if index.unique else ""
        index_type = index.type_desc.replace("_", " ")
        columns = ", ".join(self._quote_identifier(column) for column in index.columns)
        included = ""
        if index.included_columns:
            included_columns = ", ".join(self._quote_identifier(column) for column in index.included_columns)
            included = f" INCLUDE ({included_columns})"
        filter_definition = f" WHERE {index.filter_definition}" if index.filter_definition else ""
        sql = (
            f"CREATE {unique}{index_type} INDEX {self._quote_identifier(index.name)} "
            f"ON {self._table_identifier(schema, table)} ({columns}){included}{filter_definition};"
        )
        self.execute(sql)

    def create_foreign_key(
        self,
        schema: str,
        table: str,
        foreign_key: Any,
        referenced_schema: str,
        referenced_table: str,
    ) -> None:
        columns = ", ".join(self._quote_identifier(column) for column in foreign_key.columns)
        referenced_columns = ", ".join(self._quote_identifier(column) for column in foreign_key.referenced_columns)
        sql = (
            f"ALTER TABLE {self._table_identifier(schema, table)} WITH CHECK "
            f"ADD CONSTRAINT {self._quote_identifier(foreign_key.name)} FOREIGN KEY ({columns}) "
            f"REFERENCES {self._table_identifier(referenced_schema, referenced_table)} ({referenced_columns})"
        )
        if foreign_key.delete_action and foreign_key.delete_action != "NO_ACTION":
            sql += f" ON DELETE {foreign_key.delete_action.replace('_', ' ')}"
        if foreign_key.update_action and foreign_key.update_action != "NO_ACTION":
            sql += f" ON UPDATE {foreign_key.update_action.replace('_', ' ')}"
        self.execute(sql + ";")

    def add_extended_property(self, schema: str, table: str, property_metadata: Any) -> None:
        params: tuple
        if property_metadata.column_name:
            sql = (
                "EXEC sys.sp_addextendedproperty "
                "@name=%s, @value=%s, "
                "@level0type=N'SCHEMA', @level0name=%s, "
                "@level1type=N'TABLE', @level1name=%s, "
                "@level2type=N'COLUMN', @level2name=%s"
            )
            params = (
                property_metadata.name,
                property_metadata.value,
                schema,
                table,
                property_metadata.column_name,
            )
        else:
            sql = (
                "EXEC sys.sp_addextendedproperty "
                "@name=%s, @value=%s, "
                "@level0type=N'SCHEMA', @level0name=%s, "
                "@level1type=N'TABLE', @level1name=%s"
            )
            params = (property_metadata.name, property_metadata.value, schema, table)
        self.execute(sql, params)

    def try_create_index(self, schema: str, table: str, index: Any) -> None:
        try:
            self.create_index(schema, table, index)
            self._logger.info(f"Indice creado: {schema}.{table}.{index.name}")
        except Exception as exc:
            self._logger.warning(f"No se pudo crear indice {schema}.{table}.{index.name}: {exc}")

    def try_create_foreign_key(
        self,
        schema: str,
        table: str,
        foreign_key: Any,
        referenced_schema: str,
        referenced_table: str,
    ) -> None:
        try:
            self.create_foreign_key(schema, table, foreign_key, referenced_schema, referenced_table)
            self._logger.info(f"Foreign key creada: {schema}.{table}.{foreign_key.name}")
        except Exception as exc:
            self._logger.warning(f"No se pudo crear FK {schema}.{table}.{foreign_key.name}: {exc}")

    def try_add_extended_property(self, schema: str, table: str, property_metadata: Any) -> None:
        try:
            self.add_extended_property(schema, table, property_metadata)
            target = property_metadata.column_name or table
            self._logger.info(f"Comentario copiado: {schema}.{table}.{target}.{property_metadata.name}")
        except Exception as exc:
            self._logger.warning(f"No se pudo copiar comentario {schema}.{table}.{property_metadata.name}: {exc}")

    def insert_rows(self, schema: str, table: str, columns: list[str], rows: list[tuple]) -> int:
        if not rows:
            return 0

        columns_sql = ", ".join(self._quote_identifier(column) for column in columns)
        placeholders = ", ".join(["%s"] * len(columns))
        sql = f"INSERT INTO {self._table_identifier(schema, table)} ({columns_sql}) VALUES ({placeholders})"

        connection = self._get_connection()
        cursor = connection.cursor()
        try:
            cursor.executemany(sql, rows)
            connection.commit()
            return cursor.rowcount
        except Exception:
            connection.rollback()
            raise
        finally:
            cursor.close()

    def set_identity_insert(self, schema: str, table: str, enabled: bool) -> None:
        state = "ON" if enabled else "OFF"
        self.execute(f"SET IDENTITY_INSERT {self._table_identifier(schema, table)} {state}")
