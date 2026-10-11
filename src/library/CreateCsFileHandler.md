# CreateCsFileHandler

`CreateCsFileHandler` genera archivos `.cs` con la misma definicion de tabla que recibe
`ScylladbTargetDdlHandler`. No lee SQL Server, no se conecta a ScyllaDB y no ejecuta CQL.

## Crear un modelo C#

```python
from src.library.CreateCsFileHandler import CreateCsFileHandler
from src.library.ScylladbTargetDdlHandler import ScylladbColumnDefinition

handler = CreateCsFileHandler("src/Executors/outputCsNet")

columns = [
    ScylladbColumnDefinition("id_agencia", "int", cs_property_name="IdAgencia"),
    ScylladbColumnDefinition("codigo", "text", nullable=True),
]

path = handler.create_table(
    keyspace="dbo",
    table="agencias",
    columns=columns,
    partition_key=("id_agencia",),
)
```

Se crea `Agencias.cs` con atributos de `Cassandra.Mapping.Attributes`:

```csharp
[Table("agencias", ExplicitColumns = true)]
internal sealed class Agencias
{
    [PartitionKey(0)]
    [Column("id_agencia")]
    public int id_agencia { get; init; }

    [Column("codigo")]
    public string? codigo { get; init; }
}
```

## Clustering key

```python
handler.create_table(
    keyspace="dbo",
    table="movimientos",
    columns=[
        ScylladbColumnDefinition("cliente_id", "uuid"),
        ScylladbColumnDefinition("fecha", "timestamp"),
    ],
    partition_key=("cliente_id",),
    clustering_key=("fecha",),
)
```

Esto genera `[PartitionKey(0)]` para `cliente_id` y `[ClusteringKey(0)]` para `fecha`.

## Generar sin escribir

```python
content = handler.build_create_table(
    keyspace="dbo",
    table="agencias",
    columns=columns,
    partition_key=("id_agencia",),
)
```

El executor debe traducir la metadata de SQL Server una sola vez y enviar la misma definicion
a este handler y a `ScylladbTargetDdlHandler`.

Los identificadores con acentos se normalizan a ASCII antes de generar las salidas. Por ejemplo,
la columna SQL Server `cDirección` se crea como `cDireccion` en ScyllaDB y se representa como
`cDireccion` en C#.
