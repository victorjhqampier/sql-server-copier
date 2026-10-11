from __future__ import annotations

from typing import Any, Sequence


def _quote_identifier(value: str) -> str:
    if not value or not str(value).strip():
        raise ValueError("Identificador CQL no puede estar vacio")
    return f'"{str(value).replace('"', '""')}"'


def _validate_cql_type(value: str) -> str:
    cql_type = str(value).strip()
    if not cql_type:
        raise ValueError("El tipo CQL no puede estar vacio")
    if ";" in cql_type:
        raise ValueError("El tipo CQL no puede contener punto y coma")
    return cql_type


def build_create_table_cql(
    keyspace: str,
    table: str,
    columns: Sequence[Any],
    partition_key: Sequence[str],
    clustering_key: Sequence[str] = (),
) -> str:
    """Construye un CREATE TABLE CQL a partir de una definicion ya traducida."""
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
        f"{_quote_identifier(column.name)} {_validate_cql_type(column.cql_type)}" for column in columns
    ]
    quoted_partition_key = ", ".join(_quote_identifier(column) for column in partition_key)
    if len(partition_key) > 1:
        quoted_partition_key = f"({quoted_partition_key})"
    quoted_clustering_key = ", ".join(_quote_identifier(column) for column in clustering_key)
    primary_key_definition = f"PRIMARY KEY ({quoted_partition_key}"
    if quoted_clustering_key:
        primary_key_definition += f", {quoted_clustering_key}"
    primary_key_definition += ")"

    definitions = ",\n    ".join([*column_definitions, primary_key_definition])
    return (
        f"CREATE TABLE IF NOT EXISTS {_quote_identifier(keyspace)}.{_quote_identifier(table)} (\n    {definitions}\n);"
    )
