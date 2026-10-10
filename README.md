# dp-migrate-sqlserver

Copiador inteligente de **SQL Server origen -> SQL Server destino** en Python puro con `uv`. Sin Databricks, Spark, Delta ni S3.

## Que hace este proyecto

Lee tablas desde un SQL Server **origen** y las replica en un SQL Server **destino**:

1. **Copiar estructura** (DDL): columnas, tipos, longitud, precision/escala, nullable, `IDENTITY`, `DEFAULT`, `PRIMARY KEY`, indices y comentarios (extended properties).
2. **Copiar datos** (DML): lee el origen por lotes (`yield`, default 1000 registros) e inserta en el destino.

**Importante sobre el alcance:**

- Es una **copia unica**. No hay incremental, continuacion, merge ni upsert.
- La estructura **no se configura**: se descubre automaticamente desde la metadata del SQL Server origen.
- Solo se copian datos si la tabla destino **esta vacia** (`COUNT(*) = 0`). Si ya tiene datos, se omite y se registra un warning.
- Por tabla se copian como maximo `copy_rows` registros (default **17000**).
- Para volver a copiar una tabla hay que recrear la tabla destino (`--drop-if-exists`) y ejecutar de nuevo.

---

## Requisitos

| Requisito | Version |
|---|---|
| Python | 3.13+ |
| UV | 0.11+ |

---

## Instalacion

### 1. Instalar UV (solo la primera vez, por sistema)

UV se instala con un solo comando segun tu sistema operativo:

| Sistema | Comando |
|---|---|
| macOS | `curl -LsSf https://astral.sh/uv/install.sh \| sh` |
| Windows (PowerShell) | `powershell -ExecutionPolicy ByPass -c "irm https://astral.sh/uv/install.ps1 \| iex"` |
| Linux / Ubuntu | `curl -LsSf https://astral.sh/uv/install.sh \| sh` |

Despues de instalar, cerrar y abrir la terminal (o recargar el PATH) para que el comando `uv` quede disponible.

Alternativas (opcional):

- **Con Homebrew** (macOS): `brew install uv`
- **Con apt** (Ubuntu/Debian 24.04+): `apt install uv`
- **Con pip**: `pip install uv`

Verificar que quedo bien instalado:

```bash
uv --version
```

### 2. Instalar las dependencias del proyecto

Con UV ya disponible, instalar las dependencias:

```bash
uv sync --dev
```

---

## Orden de ejecucion (IMPORTANTE)

Los ejecutores deben correrse en este orden, y cada uno es una fase separada:

```text
1. FASE ESTRUCTURA  ->  uv run python -m src.Executors table
2. FASE DATOS       ->  uv run python -m src.Executors migrate
```

| Fase | Ejecutor | Que hace | Cuando |
|---|---|---|---|
| `table` | `SQLTableExecutor` | Crea schemas y tablas en el destino (DDL) | **Primero**. Sin esto no hay donde insertar |
| `migrate` | `SQLMigrateExecutor` | Copia los datos por lotes (DML) | **Despues**. Requiere que las tablas ya existan |

Detalle de lo que pasa en cada fase:

**Fase `table` (estructura), por cada tabla de la config:**

1. Lee la metadata de columnas, PK y su nombre desde el origen.
2. Crea el schema en el destino si no existe.
3. Ejecuta `CREATE TABLE` con tipos, `IDENTITY`, `DEFAULT` y `PRIMARY KEY`.
4. Crea los indices (best-effort: si uno falla, loggea y continua).
5. Copia los comentarios / extended properties (best-effort).

**Fase `migrate` (datos), por cada tabla de la config:**

1. Verifica que la tabla exista en el destino (si no, aborta esa tabla y sugiere correr `table`).
2. Si el destino ya tiene registros, omite la tabla (evita duplicados).
3. Determina el orden de copia: `order_by` config -> identity -> PK simple -> TOP sin orden (con warning).
4. Lee el origen en lotes y hace `INSERT` en el destino (con `IDENTITY_INSERT ON` si hay columnas identity).
5. Valida conteos: `origen vs insertados vs destino`.
6. **Al final de todas las tablas**, intenta crear las **foreign keys** (porque referencian tablas que debian copiarse antes).

> **Por que las FKs se crean al final?** Porque solo se copia una muestra (maximo `copy_rows`). Si una FK se creara antes de copiar datos, fallaria si la muestra no contiene los registros referenciados. Al crearse al final, el fallo se loggea y no detiene la migracion.

Errores por tabla se loggean y la ejecucion continua con la siguiente.

---

## Configuracion

### 1. Credenciales (archivo `.env`)

Copiar `.env.example` a `.env` y completar:

```bash
cp .env.example .env
```

| Variable | Descripcion |
|---|---|
| `SOURCE_SQLSERVER_HOST` | Host del SQL Server origen |
| `SOURCE_SQLSERVER_USER` | Usuario del origen |
| `SOURCE_SQLSERVER_PASSWORD` | Password del origen |
| `SOURCE_SQLSERVER_DATABASE` | Base de datos origen |
| `TARGET_SQLSERVER_HOST` | Host del SQL Server destino |
| `TARGET_SQLSERVER_USER` | Usuario del destino |
| `TARGET_SQLSERVER_PASSWORD` | Password del destino |
| `TARGET_SQLSERVER_DATABASE` | Base de datos destino |
| `BATCH_SIZE` | Registros por lote en la copia de datos (default `1000`) |
| `DROP_IF_EXISTS` | `true` elimina la tabla destino antes de recrearla; `false` la omite si existe |

Las variables se resuelven con esta prioridad (gana la primera que este presente):

```text
CLI (--SOURCE_SQLSERVER_HOST=...) > variables de entorno del sistema > archivo .env
```

### 2. Tablas a copiar (archivo `src/config/tables.py`)

La unica configuracion del proyecto. Define **que** copiar; el **como** (estructura) se infiere del origen.

```python
tables = [
    {
        "table": "dbo.Cliente",       # schema.tabla en el ORIGEN (obligatorio)
        "target_schema": "dbo",        # schema en el DESTINO (opcional, default: mismo schema)
        "copy_rows": 17000,            # maximo de registros a copiar (opcional, default: 17000)
        "order_by": "IdCliente",       # columna de orden (opcional, ver abajo)
    },
    {
        "table": "dbo.TipoCorreo",
        "target_schema": "dbo",
        "copy_rows": 17000,
    },
]
```

Opciones de cada entrada:

| Campo | Obligatorio | Default | Descripcion |
|---|---|---|---|
| `table` | Si | - | Nombre de la tabla en el origen con formato `schema.tabla` (ej: `dbo.Cliente`) |
| `target_schema` | No | schema de `table` | Schema donde se crea/copia la tabla en el destino |
| `copy_rows` | No | `17000` | Maximo de registros a copiar si el destino esta vacio. `None` copia toda la tabla |
| `order_by` | No | auto | Columna usada para elegir "los ultimos" registros. Si no se define, se usa identity, luego PK simple, luego TOP sin orden (con warning) |

**Reglas de la lista:**

- El orden de la lista define el orden de creacion y de carga (padres primero, hijos despues).
- No existe `type_tables`, `small_tables`, `watermark`, `incremental_query`, `partition_criteria`, `fields` ni `primary_key` en la config. Todo eso se descubre desde SQL Server.

---

## Uso

### Fase 1: copiar estructura (DDL)

```bash
uv run python -m src.Executors table
```

Si la tabla ya existe en el destino, se omite. Para recrearla:

```bash
uv run python -m src.Executors table --drop-if-exists
```

### Fase 2: copiar datos (DML)

```bash
uv run python -m src.Executors migrate
```

Con tamano de lote personalizado:

```bash
uv run python -m src.Executors migrate --batch-size 1000
```

### Generar estructura ScyllaDB y modelos C# (DDL)

Lee la metadata SQL Server, crea las tablas configuradas en ScyllaDB y genera los modelos C# en
`src/Executors/outputCsNet` y el mapping Kafka Connect/Zinc en `src/Executors/outputApacheZinc`:

```bash
uv run python -m src.Executors.SqlToScylladbDdlExecutor
```

Esta fase lee metadata desde `TARGET_SQLSERVER_*` y crea estructura en `SCYLLADB_*`.
Configura `SCYLLADB_CONTACT_POINTS` y `SCYLLADB_KEYSPACE` en `.env`. El keyspace debe existir;
esta fase no copia datos ni crea keyspaces.
Los mappings Zinc se generan para todas las tablas procesadas usando topics Debezium
`arify.<schema>.<tabla SQL Server>`.
Tambien se actualiza `src/Executors/outputDebezium/debezium-sqlserver.json`, solo en
`table.include.list`, y se genera `src/Executors/outputDebezium/enable-cdc.sql`
con el script CDC minimo para SQL Server, usando la misma lista de tablas.

### Ejemplo completo de principio a fin

```bash
# 0. Preparacion
cp .env.example .env   # completar credenciales
# editar src/config/tables.py con las tablas a copiar

# 1. Estructura
uv run python -m src.Executors table

# 2. Datos
uv run python -m src.Executors migrate
```

---

## Estructura del proyecto

```text
src/
  Executors/                 # ORQUESTADORES: unen config + conexiones + librerias
    __main__.py              # CLI: table | migrate (lee .env, config y lanza al ejecutor)
    SQLTableExecutor.py      # fase estructura (DDL): crea schemas, tablas, indices, comentarios
    SQLMigrateExecutor.py    # fase datos (DML): copia por lotes y crea FKs al final
  library/                   # LIBRERIAS: hacen trabajo concreto y NO leen config
    SqlServerConnector.py    # unico punto que abre y mantiene conexiones (pymssql.connect)
    SqlOriginHandler.py      # lectura origen: metadata, PK, FK, indices, comentarios, batch read
    SqlTargetHandler.py      # escritura destino: DDL, INSERT, IDENTITY_INSERT, FK/indices best-effort
    EnvironmentLibrary.py    # resolucion de variables (CLI / env / .env)
    LoggerLibrary.py         # logging a consola
  config/
    tables.py                # UNICA configuracion: lista de tablas a copiar
```

### Que hace cada libreria (para entender el flujo)

| Archivo | Responsabilidad | Metodos principales |
|---|---|---|
| `SqlServerConnector.py` | Abre y reutiliza conexiones. Una conexion por `ConnectionString`. Es el **unico** que ejecuta `pymssql.connect`. Ningun otro archivo abre conexiones | `get_connection`, `close_all` |
| `SqlOriginHandler.py` | **Solo lectura** del origen. Devuelve la estructura (columnas, tipos, identity, defaults, PK, FKs, indices, comentarios) y lee datos por lotes con `yield`. No escribe nada | `get_table_metadata`, `get_primary_key`, `get_foreign_keys`, `get_indexes`, `get_extended_properties`, `count`, `read_batches` |
| `SqlTargetHandler.py` | **Solo escritura** al destino. Crea schemas/tablas/indices/FKs/comentarios e inserta filas. Los metodos `try_*` loggean el error y continuan (best-effort) | `execute`, `ensure_schema`, `drop_table_if_exists`, `build_create_table`, `insert_rows`, `set_identity_insert`, `try_create_index`, `try_create_foreign_key` |
| `EnvironmentLibrary.py` | Resuelve la configuracion con prioridad CLI > env > `.env` | `get(*keys)` |
| `LoggerLibrary.py` | Configura el logging a consola con formato `fecha - nombre - nivel - mensaje` | `set_logger()` |

**Regla de oro:** los ejecutores orquestan, las librerias ejecutan, la config solo dice **que** copiar.

---

## Calidad

```bash
uv run ruff format .
uv run ruff check .
uv run python -m compileall -q src
```

---

## Copyright

© Arify Labs. Todos los derechos reservados.
