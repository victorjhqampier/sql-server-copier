# Tablas cuya estructura se traduce desde SQL Server hacia ScyllaDB.
# La metadata se infiere del origen; esta fase no copia datos.
# Por defecto se usa la PK SQL Server como partition_key y el nombre destino es snake_case.
# Cada entrada puede incluir keyspace, target_table, partition_key y clustering_key.
# Los tipos SQL Server geography y geometry se modelan temporalmente como text en ScyllaDB.
# El mapping Zinc usa topics arify.<schema>.<tabla SQL Server> para cada tabla procesada.

tables_scylladb = [
    # Nivel 1 - Tablas independientes
    {"table": "dbo.SI_FinRegistroNumeroTransfApp"},
    {"table": "dbo.SI_FinTipoDocumentoCTS"},
    {"table": "dbo.SI_FinDocumentoCTS"},
    {"table": "dbo.SI_FinRemuClienteCTS"},
    {"table": "dbo.SI_FinAgenciaCCE"},
    {"table": "dbo.SI_FinCanalCCE"},
    {"table": "dbo.SI_FinComisionCCE"},
    {"table": "dbo.Si_FinDirectorioCCE"},
    {"table": "dbo.SI_FinEntidadCCE"},
    {"table": "dbo.SI_FinEstConsultaCCE"},
    {"table": "dbo.SI_FinModalidadConsultaCCI"},
    {"table": "dbo.SI_FinAgencia"},
    {"table": "dbo.SI_FinCanal"},
    {"table": "dbo.SI_FinMoneda"},
    {"table": "dbo.SI_FinKardexCliOperac"},
    {"table": "dbo.SI_FinConcepto"},
    {"table": "dbo.SI_FinTipoOperacion"},
    {"table": "dbo.Si_FinEstadoKardex"},
    {"table": "dbo.SI_FinDatTipoPago"},
    {"table": "dbo.SI_FinTipoPago"},
    {"table": "dbo.SI_FinMotivoOperacion"},
    {"table": "dbo.Si_FinTipoLimite"},
    {"table": "dbo.SI_FinCaracteristicaBloq"},
    {"table": "dbo.SI_FinTipoCuenta"},
    {"table": "dbo.SI_FinAmbitoTransferencia"},
    {"table": "dbo.Si_FinClasificacionTransferencia"},
    {"table": "dbo.SI_FinDetalleOpeRechazado"},
    {"table": "dbo.SI_FinDetalleTransfKardex"},
    {"table": "dbo.SI_FinTipoTransaccion"},
    {"table": "dbo.SI_FinTipoTransferencia"},
    {"table": "dbo.SI_FinSaldoDeposito"},
    {"table": "dbo.SI_FinDetDeposCTS"},
    {"table": "dbo.SI_FinRegDocumentoCTS"},
    {"table": "dbo.SI_FinDepositoPRG"},
    {"table": "dbo.SI_FINBloqueoCuenta"},
    {"table": "dbo.SI_FinParametroProd"},
    # Nivel 2
    {"table": "dbo.SI_FinTipoTasa"},
    {"table": "dbo.SI_FinProducto"},
    {"table": "dbo.Si_FinTipoTransferenciaLimites"},
    # Nivel 3    
    {"table": "dbo.SI_FinDeposito"},
    {"table": "dbo.SI_FinKardex"},
    {"table": "dbo.Si_FinLimiteTransferencia"},
    # Nivel 4
    {"table": "dbo.SI_FinDepositoCTS"},
    {"table": "dbo.Si_FinLimiteTransferenciaConcepto"},
    {"table": "dbo.SI_FinOutboxTransaccion"},
]
