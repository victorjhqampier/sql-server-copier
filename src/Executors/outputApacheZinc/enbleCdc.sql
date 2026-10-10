-- Enables CDC only for the three approved tables and grants the Debezium login least required access.
IF DB_ID(N'$(DB_NAME)') IS NULL
    THROW 50000, 'The configured SQLSERVER_DB does not exist.', 1;
GO

USE [$(DB_NAME)];
GO

IF OBJECT_ID(N'dbo.SI_FinKardex', N'U') IS NULL
   OR OBJECT_ID(N'dbo.SI_FinAgenciaCCE', N'U') IS NULL
   OR OBJECT_ID(N'dbo.SI_FinCanalCCE', N'U') IS NULL
    THROW 50001, 'The three CDC source tables must exist before enabling CDC.', 1;
GO

IF (SELECT is_cdc_enabled FROM sys.databases WHERE name = N'$(DB_NAME)') = 0
    EXEC sys.sp_cdc_enable_db;
GO

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinKardex'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinKardex', @role_name = NULL, @supports_net_changes = 1;

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinAgenciaCCE'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinAgenciaCCE', @role_name = NULL, @supports_net_changes = 1;

IF NOT EXISTS (SELECT 1 FROM cdc.change_tables WHERE source_object_id = OBJECT_ID(N'dbo.SI_FinCanalCCE'))
    EXEC sys.sp_cdc_enable_table @source_schema = N'dbo', @source_name = N'SI_FinCanalCCE', @role_name = NULL, @supports_net_changes = 1;
GO