# CreateCqlFileHandler

`CreateCqlFileHandler` escribe el `CREATE TABLE` CQL de cada tabla ScyllaDB sin conectarse al
cluster. Reutiliza la misma definicion de columnas, partition key y clustering key usada por el
executor, el handler ScyllaDB, los modelos C# y Zinc.

## Ejemplo

```python
from src.library.CreateCqlFileHandler import CreateCqlFileHandler
from src.library.ScylladbTargetDdlHandler import ScylladbColumnDefinition

handler = CreateCqlFileHandler("src/Executors/outputApacheZinc")

handler.create_table(
    keyspace="app",
    table="si_fin_agencia",
    columns=[
        ScylladbColumnDefinition("idAgencia", "int"),
        ScylladbColumnDefinition("cDireccion", "text"),
    ],
    partition_key=("idAgencia",),
)
```

Esto actualiza `all_tables.cql` en el directorio de salida:

```sql
CREATE TABLE IF NOT EXISTS "app"."si_fin_agencia" (
    "idAgencia" int,
    "cDireccion" text,
    PRIMARY KEY ("idAgencia")
);
```
