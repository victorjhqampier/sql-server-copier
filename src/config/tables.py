# Tablas para copia SQL Server -> SQL Server.
# La configuracion solo define que copiar. La estructura se infiere desde SQL Server origen.
# No existe continuacion ni incremental: si destino tiene datos, se omite.
# copy_rows define cuantos registros copiar cuando destino esta vacio.

tables = [
    {
        "table": "dbo.Cliente",
        "target_schema": "dbo",
        "copy_rows": 17000,
    },
    {
        "table": "dbo.TipoCorreo",
        "target_schema": "dbo",
        "copy_rows": 17000,
    },
]
