from __future__ import annotations

from dataclasses import dataclass
from typing import Any, Iterator

from src.library.LoggerLibrary import LoggerLibrary
from src.library.SqlServerConnector import ConnectionString, SqlServerConnector


@dataclass(frozen=True)
class ColumnMetadata:
    name: str
    data_type: str
    max_length: int | None
    precision: int | None
    scale: int | None
    nullable: bool
    is_identity: bool
    default: str | None


@dataclass(frozen=True)
class ForeignKeyMetadata:
    name: str
    columns: list[str]
    referenced_schema: str
    referenced_table: str
    referenced_columns: list[str]
    delete_action: str
    update_action: str


@dataclass(frozen=True)
class IndexMetadata:
    name: str
    unique: bool
    type_desc: str
    columns: list[str]
    included_columns: list[str]
    filter_definition: str | None


@dataclass(frozen=True)
class ExtendedPropertyMetadata:
    name: str
    value: str
    column_name: str | None = None


def parse_table_name(table_name: str) -> tuple[str, str]:
    if not isinstance(table_name, str):
        raise ValueError("table debe ser un string con formato schema.tabla")
    parts = table_name.split(".", 1)
    if len(parts) != 2 or not parts[0] or not parts[1]:
        raise ValueError(f"table debe tener formato schema.tabla, por ejemplo dbo.Cliente: {table_name}")
    return parts[0], parts[1]


class SqlOriginHandler:
    """Lectura exclusiva desde SQL Server origen.

    No abre conexiones. Toda conexion se obtiene desde SqlServerConnector.
    No escribe, no crea tablas y no modifica datos.
    """

    def __init__(
        self,
        connection_string: ConnectionString,
        connector: SqlServerConnector,
        logger: Any = None,
        batch_size: int = 1000,
    ) -> None:
        if batch_size <= 0:
            raise ValueError("batch_size debe ser mayor a 0")
        self._connection_string = connection_string
        self._connector = connector
        self._logger = logger or LoggerLibrary.set_logger().getChild(self.__class__.__name__)
        self._batch_size = batch_size

    def _get_connection(self):
        return self._connector.get_connection(self._connection_string)

    @staticmethod
    def _quote_identifier(value: str) -> str:
        if not value or not str(value).strip():
            raise ValueError("Identificador SQL no puede estar vacio")
        return f"[{str(value).replace(']', ']]')}]"

    def _table_identifier(self, schema: str, table: str) -> str:
        return ".".join([self._quote_identifier(schema), self._quote_identifier(table)])

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

    def get_table_metadata(self, schema: str, table: str) -> list[ColumnMetadata]:
        sql = (
            "SELECT c.COLUMN_NAME, c.DATA_TYPE, c.CHARACTER_MAXIMUM_LENGTH, "
            "c.NUMERIC_PRECISION, c.NUMERIC_SCALE, c.IS_NULLABLE, c.COLUMN_DEFAULT, "
            "COLUMNPROPERTY(OBJECT_ID(QUOTENAME(c.TABLE_SCHEMA) + '.' + QUOTENAME(c.TABLE_NAME)), "
            "c.COLUMN_NAME, 'IsIdentity') AS IS_IDENTITY "
            "FROM INFORMATION_SCHEMA.COLUMNS c "
            "WHERE c.TABLE_SCHEMA = %s AND c.TABLE_NAME = %s "
            "ORDER BY c.ORDINAL_POSITION"
        )
        rows = self._query_all(sql, (schema, table))
        return [
            ColumnMetadata(
                name=row[0],
                data_type=row[1],
                max_length=row[2],
                precision=row[3],
                scale=row[4],
                nullable=str(row[5]).strip().upper() == "YES",
                is_identity=bool(row[6]),
                default=row[7],
            )
            for row in rows
        ]

    def get_column_names(self, schema: str, table: str) -> list[str]:
        return [column.name for column in self.get_table_metadata(schema, table)]

    def get_primary_key(self, schema: str, table: str) -> list[str]:
        sql = (
            "SELECT kcu.COLUMN_NAME "
            "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc "
            "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu "
            "    ON tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME "
            "   AND tc.TABLE_SCHEMA = kcu.TABLE_SCHEMA "
            "WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY' "
            "  AND tc.TABLE_SCHEMA = %s AND tc.TABLE_NAME = %s "
            "ORDER BY kcu.ORDINAL_POSITION"
        )
        rows = self._query_all(sql, (schema, table))
        return [row[0] for row in rows]

    def get_primary_key_name(self, schema: str, table: str) -> str | None:
        sql = (
            "SELECT tc.CONSTRAINT_NAME "
            "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc "
            "WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY' "
            "  AND tc.TABLE_SCHEMA = %s AND tc.TABLE_NAME = %s"
        )
        row = self._query_one(sql, (schema, table))
        return row[0] if row else None

    def get_identity_columns(self, schema: str, table: str) -> list[str]:
        return [column.name for column in self.get_table_metadata(schema, table) if column.is_identity]

    def resolve_copy_order_column(self, schema: str, table: str, order_by: str | None = None) -> str | None:
        if order_by:
            return order_by

        identity_columns = self.get_identity_columns(schema, table)
        if identity_columns:
            return identity_columns[0]

        primary_key = self.get_primary_key(schema, table)
        if len(primary_key) == 1:
            return primary_key[0]

        self._logger.warning(
            f"No se encontro order_by, identity ni PK simple para {schema}.{table}. La copia TOP no tendra orden deterministico"
        )
        return None

    def get_foreign_keys(self, schema: str, table: str) -> list[ForeignKeyMetadata]:
        sql = (
            "SELECT fk.name, pc.name, rs.name, rt.name, rc.name, "
            "fk.delete_referential_action_desc, fk.update_referential_action_desc, fkc.constraint_column_id "
            "FROM sys.foreign_keys fk "
            "INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id "
            "INNER JOIN sys.tables pt ON fk.parent_object_id = pt.object_id "
            "INNER JOIN sys.schemas ps ON pt.schema_id = ps.schema_id "
            "INNER JOIN sys.columns pc ON fkc.parent_object_id = pc.object_id AND fkc.parent_column_id = pc.column_id "
            "INNER JOIN sys.tables rt ON fk.referenced_object_id = rt.object_id "
            "INNER JOIN sys.schemas rs ON rt.schema_id = rs.schema_id "
            "INNER JOIN sys.columns rc ON fkc.referenced_object_id = rc.object_id AND fkc.referenced_column_id = rc.column_id "
            "WHERE ps.name = %s AND pt.name = %s "
            "ORDER BY fk.name, fkc.constraint_column_id"
        )
        rows = self._query_all(sql, (schema, table))
        grouped: dict[str, dict[str, Any]] = {}
        for row in rows:
            name = row[0]
            item = grouped.setdefault(
                name,
                {
                    "columns": [],
                    "referenced_schema": row[2],
                    "referenced_table": row[3],
                    "referenced_columns": [],
                    "delete_action": row[5],
                    "update_action": row[6],
                },
            )
            item["columns"].append(row[1])
            item["referenced_columns"].append(row[4])

        return [
            ForeignKeyMetadata(
                name=name,
                columns=item["columns"],
                referenced_schema=item["referenced_schema"],
                referenced_table=item["referenced_table"],
                referenced_columns=item["referenced_columns"],
                delete_action=item["delete_action"],
                update_action=item["update_action"],
            )
            for name, item in grouped.items()
        ]

    def get_indexes(self, schema: str, table: str) -> list[IndexMetadata]:
        sql = (
            "SELECT i.name, i.is_unique, i.type_desc, c.name, ic.is_included_column, ic.key_ordinal, "
            "ic.index_column_id, i.filter_definition "
            "FROM sys.indexes i "
            "INNER JOIN sys.tables t ON i.object_id = t.object_id "
            "INNER JOIN sys.schemas s ON t.schema_id = s.schema_id "
            "INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id "
            "INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id "
            "WHERE s.name = %s AND t.name = %s "
            "  AND i.is_primary_key = 0 AND i.is_unique_constraint = 0 AND i.type_desc <> 'HEAP' "
            "ORDER BY i.name, ic.is_included_column, ic.key_ordinal, ic.index_column_id"
        )
        rows = self._query_all(sql, (schema, table))
        grouped: dict[str, dict[str, Any]] = {}
        for row in rows:
            name = row[0]
            item = grouped.setdefault(
                name,
                {
                    "unique": bool(row[1]),
                    "type_desc": row[2],
                    "columns": [],
                    "included_columns": [],
                    "filter_definition": row[7],
                },
            )
            if row[4]:
                item["included_columns"].append(row[3])
            else:
                item["columns"].append(row[3])

        return [
            IndexMetadata(
                name=name,
                unique=item["unique"],
                type_desc=item["type_desc"],
                columns=item["columns"],
                included_columns=item["included_columns"],
                filter_definition=item["filter_definition"],
            )
            for name, item in grouped.items()
            if item["columns"]
        ]

    def get_extended_properties(self, schema: str, table: str) -> list[ExtendedPropertyMetadata]:
        sql = (
            "SELECT ep.name, CONVERT(NVARCHAR(MAX), ep.value), c.name "
            "FROM sys.extended_properties ep "
            "INNER JOIN sys.tables t ON ep.major_id = t.object_id "
            "INNER JOIN sys.schemas s ON t.schema_id = s.schema_id "
            "LEFT JOIN sys.columns c ON ep.major_id = c.object_id AND ep.minor_id = c.column_id "
            "WHERE s.name = %s AND t.name = %s AND ep.class = 1 "
            "ORDER BY ep.minor_id, ep.name"
        )
        rows = self._query_all(sql, (schema, table))
        return [ExtendedPropertyMetadata(name=row[0], value=str(row[1]), column_name=row[2]) for row in rows]

    def count(self, schema: str, table: str) -> int:
        row = self._query_one(f"SELECT COUNT(*) FROM {self._table_identifier(schema, table)}")
        return int(row[0]) if row else 0

    def read_batches(
        self,
        schema: str,
        table: str,
        columns: list[str],
        batch_size: int | None = None,
        limit: int | None = None,
        order_by: str | None = None,
    ) -> Iterator[list[tuple]]:
        size = batch_size or self._batch_size
        if size <= 0:
            raise ValueError("batch_size debe ser mayor a 0")
        if limit is not None and limit <= 0:
            raise ValueError("limit debe ser mayor a 0")
        if not columns:
            raise ValueError("columns no puede estar vacio")

        select = ", ".join(self._quote_identifier(column) for column in columns)
        table_identifier = self._table_identifier(schema, table)
        if limit is None:
            sql = f"SELECT {select} FROM {table_identifier}"
            if order_by:
                sql += f" ORDER BY {self._quote_identifier(order_by)} ASC"
        else:
            if order_by:
                quoted_order = self._quote_identifier(order_by)
                sql = (
                    f"SELECT {select} FROM ("
                    f"SELECT TOP ({int(limit)}) {select} FROM {table_identifier} ORDER BY {quoted_order} DESC"
                    f") AS source_copy ORDER BY {quoted_order} ASC"
                )
            else:
                sql = f"SELECT TOP ({int(limit)}) {select} FROM {table_identifier}"

        cursor = self._get_connection().cursor()
        try:
            cursor.execute(sql)
            while True:
                rows = cursor.fetchmany(size)
                if not rows:
                    break
                yield list(rows)
        finally:
            cursor.close()
