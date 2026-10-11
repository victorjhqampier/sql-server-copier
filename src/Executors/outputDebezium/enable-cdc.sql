-- Generado por CreateDebeziumMapHandler.
-- Habilita CDC en SQL Server para las tablas procesadas.

USE [$(DB_NAME)];
GO

IF (SELECT is_cdc_enabled FROM sys.databases WHERE name = DB_NAME()) = 0
    EXEC sys.sp_cdc_enable_db;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinRegistroNumeroTransfApp'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinRegistroNumeroTransfApp', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinTipoDocumentoCTS'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinTipoDocumentoCTS', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinDocumentoCTS'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinDocumentoCTS', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinRemuClienteCTS'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinRemuClienteCTS', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinAgenciaCCE'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinAgenciaCCE', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinCanalCCE'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinCanalCCE', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinComisionCCE'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinComisionCCE', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.Si_FinDirectorioCCE'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'Si_FinDirectorioCCE', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinEntidadCCE'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinEntidadCCE', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinEstConsultaCCE'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinEstConsultaCCE', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinModalidadConsultaCCI'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinModalidadConsultaCCI', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinAgencia'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinAgencia', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinCanal'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinCanal', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinMoneda'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinMoneda', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinKardexCliOperac'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinKardexCliOperac', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinConcepto'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinConcepto', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinTipoOperacion'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinTipoOperacion', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.Si_FinEstadoKardex'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'Si_FinEstadoKardex', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinDatTipoPago'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinDatTipoPago', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinTipoPago'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinTipoPago', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinMotivoOperacion'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinMotivoOperacion', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.Si_FinTipoLimite'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'Si_FinTipoLimite', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinCaracteristicaBloq'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinCaracteristicaBloq', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinTipoCuenta'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinTipoCuenta', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinAmbitoTransferencia'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinAmbitoTransferencia', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.Si_FinClasificacionTransferencia'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'Si_FinClasificacionTransferencia', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinDetalleOpeRechazado'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinDetalleOpeRechazado', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinDetalleTransfKardex'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinDetalleTransfKardex', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinTipoTransaccion'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinTipoTransaccion', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinTipoTransferencia'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinTipoTransferencia', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinSaldoDeposito'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinSaldoDeposito', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinDetDeposCTS'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinDetDeposCTS', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinRegDocumentoCTS'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinRegDocumentoCTS', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinDepositoPRG'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinDepositoPRG', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FINBloqueoCuenta'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FINBloqueoCuenta', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinParametroProd'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinParametroProd', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinTipoTasa'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinTipoTasa', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinProducto'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinProducto', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.Si_FinTipoTransferenciaLimites'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'Si_FinTipoTransferenciaLimites', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinDeposito'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinDeposito', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinKardex'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinKardex', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.Si_FinLimiteTransferencia'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'Si_FinLimiteTransferencia', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinDepositoCTS'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinDepositoCTS', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.Si_FinLimiteTransferenciaConcepto'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'Si_FinLimiteTransferenciaConcepto', @role_name = NULL, @supports_net_changes = 0;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinOutboxTransaccion'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinOutboxTransaccion', @role_name = NULL, @supports_net_changes = 0;
GO
