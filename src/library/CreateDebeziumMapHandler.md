# CreateDebeziumMapHandler

`CreateDebeziumMapHandler` actualiza `table.include.list` dentro de
`src/Executors/outputDebezium/debezium-sqlserver.json` usando la misma lista de tablas que procesa
`SqlToScylladbDdlExecutor`.

No recrea la configuracion Debezium completa y no modifica credenciales, transforms, converters,
`topic.prefix` ni schema history.

## Ejemplo

```python
from src.library.CreateDebeziumMapHandler import CreateDebeziumMapHandler

handler = CreateDebeziumMapHandler("src/Executors/outputDebezium/debezium-sqlserver.json")

handler.update_table_include_list([
    {"table": "dbo.SI_FinKardex"},
    {"table": "dbo.SI_FinAgenciaCCE"},
])
```

Resultado:

```json
{
  "table.include.list": "dbo\\.SI_FinKardex,dbo\\.SI_FinAgenciaCCE"
}
```

Debezium usa `topic.prefix = "arify"`, por eso esos nombres producen topics como
`arify.dbo.SI_FinKardex`.
