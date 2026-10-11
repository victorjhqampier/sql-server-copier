from __future__ import annotations

import re
import unicodedata
from pathlib import Path
from typing import Sequence

from src.library.ScylladbTargetDdlHandler import ScylladbColumnDefinition


class CreateCsFileHandler:
    """Genera clases C# a partir de la misma definicion usada para crear tablas ScyllaDB."""

    _CS_TYPES = {
        "ascii": "string",
        "bigint": "long",
        "blob": "byte[]",
        "boolean": "bool",
        "date": "Cassandra.LocalDate",
        "decimal": "decimal",
        "double": "double",
        "float": "float",
        "inet": "System.Net.IPAddress",
        "int": "int",
        "smallint": "short",
        "text": "string",
        "timestamp": "System.DateTime",
        "timeuuid": "System.Guid",
        "tinyint": "sbyte",
        "uuid": "System.Guid",
        "varchar": "string",
    }

    def __init__(
        self,
        output_dir: Path | str,
        namespace: str = "ScylladbInfrastructure.Collections.Tables",
    ) -> None:
        self._output_dir = Path(output_dir)
        self._namespace = namespace.strip()
        if not self._namespace:
            raise ValueError("namespace no puede estar vacio")

    @staticmethod
    def _pascal_case(value: str) -> str:
        value = unicodedata.normalize("NFKD", value).encode("ascii", "ignore").decode("ascii")
        words = re.sub(r"([a-z0-9])([A-Z])", r"\1 \2", value)
        words = re.sub(r"([A-Z]+)([A-Z][a-z])", r"\1 \2", words)
        parts = [part for part in re.split(r"[^A-Za-z0-9]+", words) if part]
        if not parts:
            raise ValueError("El nombre no puede estar vacio")
        return "".join(part[:1].upper() + part[1:].lower() for part in parts)

    @staticmethod
    def _validate_definition(
        columns: Sequence[ScylladbColumnDefinition],
        partition_key: Sequence[str],
        clustering_key: Sequence[str],
    ) -> None:
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

    def _cs_type_for(self, column: ScylladbColumnDefinition) -> str:
        cs_type = column.cs_type or self._CS_TYPES.get(column.cql_type.lower(), "object")
        if column.nullable and not cs_type.endswith("?"):
            return f"{cs_type}?"
        return cs_type

    def _property_definition(
        self,
        column: ScylladbColumnDefinition,
        partition_key: Sequence[str],
        clustering_key: Sequence[str],
    ) -> str:
        attributes: list[str] = []
        if column.name in partition_key:
            attributes.append(f"[PartitionKey({partition_key.index(column.name)})]")
        if column.name in clustering_key:
            attributes.append(f"[ClusteringKey({clustering_key.index(column.name)})]")
        attributes.append(f'[Column("{column.name}")]')
        property_name = column.cs_property_name or column.name
        return "\n".join([*attributes, f"public {self._cs_type_for(column)} {property_name} {{ get; init; }}"])

    def build_create_table(
        self,
        keyspace: str,
        table: str,
        columns: Sequence[ScylladbColumnDefinition],
        partition_key: Sequence[str],
        clustering_key: Sequence[str] = (),
    ) -> str:
        """Construye la clase C# equivalente a la tabla ScyllaDB sin escribirla."""
        self._validate_definition(columns, partition_key, clustering_key)
        _ = keyspace
        class_name = self._pascal_case(table)
        properties = "\n\n    ".join(
            self._property_definition(column, partition_key, clustering_key) for column in columns
        )
        return (
            "using Cassandra.Mapping.Attributes;\n\n"
            f"namespace {self._namespace};\n\n"
            f'[Table("{table}", ExplicitColumns = true)]\n'
            f"internal sealed class {class_name}\n"
            "{\n"
            f"    {properties}\n"
            "}\n"
        )

    def create_table(
        self,
        keyspace: str,
        table: str,
        columns: Sequence[ScylladbColumnDefinition],
        partition_key: Sequence[str],
        clustering_key: Sequence[str] = (),
    ) -> Path:
        """Genera o reemplaza el archivo C# asociado a una tabla ScyllaDB."""
        content = self.build_create_table(keyspace, table, columns, partition_key, clustering_key)
        output_path = self._output_dir / f"{self._pascal_case(table)}.cs"
        output_path.parent.mkdir(parents=True, exist_ok=True)
        output_path.write_text(content, encoding="utf-8")
        return output_path
