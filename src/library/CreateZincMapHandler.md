# CreateZincMapHandler

`CreateZincMapHandler` genera `scylladb-sink.json` para el Cassandra Sink Connector de Kafka
Connect. La cabecera del connector es fija; cada llamada agrega un topic y su mapping ScyllaDB.

## Uso basico

```python
from src.library.CreateZincMapHandler import CreateZincMapHandler
from src.library.ScylladbTargetDdlHandler import ScylladbColumnDefinition

handler = CreateZincMapHandler("src/Executors/outputApacheZinc")

handler.create_table(
    keyspace="dbo",
    table="si_fin_kardex",
    columns=[
        ScylladbColumnDefinition("idkardex", "int"),
        ScylladbColumnDefinition("dfechaope", "timestamp"),
    ],
    partition_key=("idkardex",),
    topic="arify.dbo.SI_FinKardex",
)
```

Genera `scylladb-sink.json` con:

```json
{
  "topics": "arify.dbo.SI_FinKardex",
  "topic.arify.dbo.SI_FinKardex.dbo.si_fin_kardex.mapping": "idkardex=key.idkardex,dfechaope=value.dfechaope"
}
```

Las columnas de `partition_key` y `clustering_key` se mapean desde `key`; las demas desde
`value`. Esto asume que Debezium aplico `ExtractNewRecordState` y expone los campos directamente.

Cuando el nombre fuente tiene acentos, el nombre de la columna ScyllaDB se normaliza a ASCII y
el mapping conserva el nombre original del evento Debezium. Por ejemplo,
`cDireccion=value.cDirección` escribe el campo fuente `cDirección` en la columna ScyllaDB
`cDireccion`.

## Prefijo de topic

```python
handler = CreateZincMapHandler(
    "src/Executors/outputApacheZinc",
    topic_prefix="db_transaction.dbo",
)
```

Con ese prefijo, la tabla `si_fin_kardex` usa el topic `db_transaction.dbo.si_fin_kardex`.
