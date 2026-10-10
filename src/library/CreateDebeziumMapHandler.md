# CreateDebeziumMapHandler

`CreateDebeziumMapHandler` actualiza `table.include.list` dentro de
`src/Executors/outputDebezium/debezium-sqlserver.json` y genera el script
`src/Executors/outputDebezium/enable-cdc.sql` para habilitar CDC en SQL Server,
usando la misma lista de tablas que procesa `SqlToScylladbDdlExecutor`.

No recrea la configuracion Debezium completa y no modifica credenciales, transforms, converters,
`topic.prefix` ni schema history.

## Ejemplo

```python
from src.library.CreateDebeziumMapHandler import CreateDebeziumMapHandler

handler = CreateDebeziumMapHandler(
    "src/Executors/outputDebezium/debezium-sqlserver.json",
    "src/Executors/outputDebezium/enable-cdc.sql",
)

# Sincroniza ambos archivos (JSON + SQL)
debezium_path, cdc_sql_path = handler.sync([
    {"table": "dbo.SI_FinKardex"},
    {"table": "dbo.SI_FinAgenciaCCE"},
])
```

Resultado en `debezium-sqlserver.json`:

```json
{
  "table.include.list": "dbo\\.SI_FinKardex,dbo\\.SI_FinAgenciaCCE"
}
```

Resultado en `enable-cdc.sql`:

```sql
USE [$(DB_NAME)];
GO

IF (SELECT is_cdc_enabled FROM sys.databases WHERE name = DB_NAME()) = 0
    EXEC sys.sp_cdc_enable_db;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinKardex'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinKardex', @role_name = NULL, @supports_net_changes = 0;
GO
```

Debezium usa `topic.prefix = "arify"`, por eso esos nombres producen topics como
`arify.dbo.SI_FinKardex`.
