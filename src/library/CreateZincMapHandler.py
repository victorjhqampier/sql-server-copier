from __future__ import annotations

import json
from pathlib import Path
from typing import Sequence

from src.library.ScylladbTargetDdlHandler import ScylladbColumnDefinition


class CreateZincMapHandler:
    """Genera la configuracion Kafka Connect Sink para tablas ScyllaDB."""

    _STATIC_CONFIGURATION = {
        "connector.class": "com.datastax.oss.kafka.sink.CassandraSinkConnector",
        "tasks.max": "1",
        "contactPoints": "${env:SCYLLA_CONTACT_POINTS}",
        "loadBalancing.localDc": "${env:SCYLLA_LOCAL_DC}",
        "auth.username": "${env:SCYLLA_SINK_USERNAME}",
        "auth.password": "${env:SCYLLA_SINK_PASSWORD}",
        "deletesEnabled": "true",
    }

    def __init__(self, output_dir: Path | str, topic_prefix: str | None = None) -> None:
        self._output_dir = Path(output_dir)
        self._topic_prefix = topic_prefix.strip(". ") if topic_prefix else None
        if topic_prefix is not None and not self._topic_prefix:
            raise ValueError("topic_prefix no puede estar vacio")
        self._topics: list[str] = []
        self._mappings: dict[str, str] = {}

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
        if len(column_names) != len(columns):
            raise ValueError("columns no puede repetir nombres")
        primary_key = [*partition_key, *clustering_key]
        if len(primary_key) != len(set(primary_key)):
            raise ValueError("partition_key y clustering_key no pueden repetir columnas")
        missing_columns = set(primary_key) - column_names
        if missing_columns:
            raise ValueError(f"Las columnas de PRIMARY KEY no existen: {', '.join(sorted(missing_columns))}")

    def _topic_for(self, table: str) -> str:
        if not table or not table.strip():
            raise ValueError("table no puede estar vacia")
        return f"{self._topic_prefix}.{table}" if self._topic_prefix else table

    def build_create_table(
        self,
        keyspace: str,
        table: str,
        columns: Sequence[ScylladbColumnDefinition],
        partition_key: Sequence[str],
        clustering_key: Sequence[str] = (),
    ) -> str:
        """Construye el mapping Zinc para una tabla sin escribir el archivo JSON."""
        self._validate_definition(columns, partition_key, clustering_key)
        if not keyspace or not keyspace.strip():
            raise ValueError("keyspace no puede estar vacio")

        key_columns = set(partition_key) | set(clustering_key)
        return ",".join(
            f"{column.name}={'key' if column.name in key_columns else 'value'}.{column.name}" for column in columns
        )

    def create_table(
        self,
        keyspace: str,
        table: str,
        columns: Sequence[ScylladbColumnDefinition],
        partition_key: Sequence[str],
        clustering_key: Sequence[str] = (),
        *,
        topic: str | None = None,
    ) -> Path:
        """Agrega el mapping de una tabla y escribe el archivo scylladb-sink.json."""
        mapping = self.build_create_table(keyspace, table, columns, partition_key, clustering_key)
        topic_name = topic.strip() if topic else self._topic_for(table)
        if not topic_name:
            raise ValueError("topic no puede estar vacio")
        if topic_name not in self._topics:
            self._topics.append(topic_name)
        mapping_key = f"topic.{topic_name}.{keyspace}.{table}.mapping"
        self._mappings[mapping_key] = mapping

        configuration = {
            **self._STATIC_CONFIGURATION,
            "topics": ",".join(self._topics),
            **self._mappings,
        }
        output_path = self._output_dir / "scylladb-sink.json"
        output_path.parent.mkdir(parents=True, exist_ok=True)
        output_path.write_text(f"{json.dumps(configuration, indent=2)}\n", encoding="utf-8")
        return output_path
