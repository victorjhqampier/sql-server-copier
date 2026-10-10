# ScylladbTargetDdlHandler

`ScylladbTargetDdlHandler` crea tablas en ScyllaDB. Usa una sesion entregada por
`ScylladbConnector`; no abre ni cierra conexiones, no lee SQL Server y no traduce tipos.

## Crear una tabla

```python
from src.library.ScylladbConnector import ScylladbConnectionString, ScylladbConnector
from src.library.ScylladbTargetDdlHandler import ScylladbColumnDefinition, ScylladbTargetDdlHandler

connection_string = ScylladbConnectionString(
    contact_points=("127.0.0.1",),
    keyspace="dbo",
)
connector = ScylladbConnector()
handler = ScylladbTargetDdlHandler(connection_string, connector)

columns = [
    ScylladbColumnDefinition("id_agencia", "int"),
    ScylladbColumnDefinition("codigo", "text"),
    ScylladbColumnDefinition("vigente", "boolean"),
]

handler.create_table(
    keyspace="dbo",
    table="agencias",
    columns=columns,
    partition_key=("id_agencia",),
)
```

El handler ejecuta el CQL:

```sql
CREATE TABLE IF NOT EXISTS "dbo"."agencias" (
    "id_agencia" int,
    "codigo" text,
    "vigente" boolean,
    PRIMARY KEY ("id_agencia")
);
```

## Clave de clustering

```python
handler.create_table(
    keyspace="dbo",
    table="movimientos",
    columns=[
        ScylladbColumnDefinition("cliente_id", "uuid"),
        ScylladbColumnDefinition("fecha", "timestamp"),
        ScylladbColumnDefinition("monto", "decimal"),
    ],
    partition_key=("cliente_id",),
    clustering_key=("fecha",),
)
```

Esto genera `PRIMARY KEY ("cliente_id", "fecha")`.

## Generar CQL sin ejecutarlo

```python
cql = handler.build_create_table(
    keyspace="dbo",
    table="agencias",
    columns=columns,
    partition_key=("id_agencia",),
)
```

El futuro executor debe obtener y traducir la metadata SQL Server antes de llamar a este handler.
