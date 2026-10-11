CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_registro_numero_transf_app" (
    "idRegistroNumeroTransfApp" int,
    "cTelefonoApp" text,
    "idCli" int,
    "idTipoDocumento" int,
    "cDocumentoID" text,
    "idCanal" int,
    "idClienteApp" text,
    "idModulo" int,
    "dFechaRegistro" timestamp,
    "lTransfInteroperabilidad" boolean,
    "idCuenta" int,
    "cNumeroCCI" text,
    "lVigente" boolean,
    PRIMARY KEY ("idRegistroNumeroTransfApp")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_tipo_documento_cts" (
    "idTipoDocumentoCTS" int,
    "cTipoDocumentoCTS" text,
    "lEstado" boolean,
    PRIMARY KEY ("idTipoDocumentoCTS")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_documento_cts" (
    "idDocumentoCTS" int,
    "cDocumentoCTS" text,
    "idTipoDocumentoCTS" int,
    "lEstado" boolean,
    PRIMARY KEY ("idDocumentoCTS")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_remu_cliente_cts" (
    "idCuenta" int,
    "idPeriodo" int,
    "idMoneda" int,
    "nRemuTotal" decimal,
    PRIMARY KEY (("idCuenta", "idPeriodo"))
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_agencia_cce" (
    "idAgenciaCCE" int,
    "cCodigoRegistro" text,
    "cNumeroEntidad" text,
    "cNumeroOficina" text,
    "cNumeroOficinaPlaza" text,
    "cUbigeoPlazaCAnexo" text,
    "cUbigeoPlazaCTransf" text,
    "cNombreOficina" text,
    "cNombreDomicilioOfi" text,
    "cNombreLocalidad" text,
    "cCodigoPostDistrito" text,
    "cNombrePlaza" text,
    "cTipoOficina" text,
    "cUbigeoINEI" text,
    "cPlazaExclusiva" text,
    "cPrefTelef" text,
    "cNumTelef1" text,
    "cNumTelef2" text,
    "cNumTelefFax" text,
    "cNumTelefCentral" text,
    "cNumExtension" text,
    "cMotivoActualizacion" text,
    "cFechaActualizacion" text,
    "lVigente" boolean,
    PRIMARY KEY ("idAgenciaCCE")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_canal_cce" (
    "idCanalCCE" int,
    "cCanalCCE" text,
    "cCodigo" text,
    "lVigente" boolean,
    PRIMARY KEY ("idCanalCCE")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_comision_cce" (
    "idComisionCCE" int,
    "cCodigo" text,
    "cCanalAndes" text,
    "idCanal" int,
    "cTipo" text,
    "cOrigen" text,
    "cDestino" text,
    "dPeriodoInicial" timestamp,
    "dPeriodoFinal" timestamp,
    "cTipOpeOriginante" text,
    "cTipoTransferencia" text,
    "cModalidad" text,
    "cTipoPersona" text,
    "cCanal" text,
    "cCodConcepDebito" text,
    "cMoneda" text,
    "idMoneda" int,
    "nMontoMin" decimal,
    "nMontoMax" decimal,
    "nComisionVariable" decimal,
    "nComisionVarMin" decimal,
    "nComisionVarMax" decimal,
    "nComisionFija" decimal,
    "cPagadoPor" text,
    "idUsuReg" int,
    "dFechaRegistro" timestamp,
    "dFechaModificacion" timestamp,
    "lVigente" boolean,
    PRIMARY KEY ("idComisionCCE")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_directorio_cce" (
    "idDirectorioCCE" int,
    "cNombre" text,
    "cCodigo" text,
    "lVigente" boolean,
    PRIMARY KEY ("idDirectorioCCE")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_entidad_cce" (
    "idEntidadCCE" int,
    "cNombre" text,
    "cNombreCorto" text,
    "cAbreviatura" text,
    "cCodigoCCE" text,
    "lDirectorio" boolean,
    "lVigente" boolean,
    PRIMARY KEY ("idEntidadCCE")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_est_consulta_cce" (
    "idEstConsultaCCE" int,
    "idTipoOperacion" int,
    "idEntidadOperacion" int,
    "idAmbitoTransferencia" int,
    "idModalidadConsultaCCI" int,
    "cNumeroCCIOrigen" text,
    "cNumeroCCIDestino" text,
    "cCodigoOperacion" text,
    "cNumeroCelDestino" text,
    "lRechazadoCCE" boolean,
    "cCodigoRechazado" text,
    "cCodigoEstado" text,
    "dFechaHoraOpe" timestamp,
    "idCanal" int,
    "cDocumentoID" text,
    "idMoneda" int,
    "lVigente" boolean,
    PRIMARY KEY ("idEstConsultaCCE")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_modalidad_consulta_cci" (
    "idModalidadConsultaCCI" int,
    "cModalidadConsultaCCI" text,
    "lVigente" boolean,
    PRIMARY KEY ("idModalidadConsultaCCI")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_agencia" (
    "idAgencia" int,
    "cNombreAge" text,
    "cNomCorto" text,
    "idUbigeo" int,
    "cDireccion" text,
    "cRefDirec" text,
    "cFono" text,
    "lEstado" boolean,
    "cCodSBS" text,
    "idTipoOficina" int,
    "dfechaCreacion" timestamp,
    "cNroResolucion" text,
    "idTipEsquemaCaja" int,
    "nAnioResolucion" int,
    "lAplicaGiros" boolean,
    "idCategoria" int,
    PRIMARY KEY ("idAgencia")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_canal" (
    "idCanal" int,
    "cCanal" text,
    "lVigente" boolean,
    "cCodSBS" text,
    PRIMARY KEY ("idCanal")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_moneda" (
    "idMoneda" int,
    "cMoneda" text,
    "lVigente" boolean,
    "CodigoISO" text,
    "cCodSBS" text,
    "cSimbolo" text,
    "cContable" text,
    "lComision" boolean,
    "cCodCCE" text,
    PRIMARY KEY ("idMoneda")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_kardex_cli_operac" (
    "idKardex" int,
    "idCli" int,
    "lVigente" boolean,
    PRIMARY KEY (("idKardex", "idCli", "lVigente"))
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_concepto" (
    "idConcepto" int,
    "cConcepto" text,
    "lVigente" boolean,
    "cNombreCorto" text,
    "idGrupoCon" int,
    "lAplicaContable" boolean,
    "nOrden" int,
    PRIMARY KEY ("idConcepto")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_tipo_operacion" (
    "idTipoOperacion" int,
    "cTipoOperacion" text,
    "lVigente" boolean,
    "idModulo" int,
    "lIndClaModalidad" boolean,
    "lIndClaMoneda" boolean,
    "lIndClaProducto" boolean,
    "lIndExtractoCta" boolean,
    "cCodSBS" text,
    "idTipoAsiento" int,
    "nNumIpresionVou" int,
    "lRegOpeSplaft" boolean,
    "cDesOpeSplaft" text,
    "lImprimir" boolean,
    "nNumDiasVcto" int,
    PRIMARY KEY ("idTipoOperacion")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_estado_kardex" (
    "idEstadoKardex" int,
    "cEstadoKardex" text,
    "lVigente" boolean,
    "lTipoEstadoOpe" boolean,
    PRIMARY KEY ("idEstadoKardex")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_dat_tipo_pago" (
    "idkardex" int,
    "idTipoValorado" int,
    "idCuenta" int,
    "cNroCuentaDoc" text,
    "cNroDocumento" text,
    "cSerieDocumento" text,
    "idEntidad" int,
    "dFechaReg" date,
    "nDiasValoriz" int,
    "nEstadoValoriz" int,
    "idAgencia" int,
    "idUsuarioVal" int,
    "dFechaVal" date,
    "cMotivo" text,
    "lVigente" boolean,
    "idCuentaRel" int,
    "idMoneda" int,
    "nMontoDoc" decimal,
    "dFechaEmiDoc" date,
    "nNroFolio" int,
    "idEntidadDestino" int,
    "cNroCtaDestino" text,
    "lisMismaCta" boolean,
    "dFecHorReg" timestamp,
    "idUsuActDatos" int,
    "dFechaActualiza" date,
    "dFecHoraActualiza" timestamp,
    "lIndicaActDatos" boolean,
    PRIMARY KEY (("idkardex", "idTipoValorado", "idCuenta"))
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_tipo_pago" (
    "idTipoPago" int,
    "cDesTipoPago" text,
    "lVigente" boolean,
    "lOpeRetiro" boolean,
    "lOpeApertura" boolean,
    "cCodSBS" text,
    "lOpePagoComPago" boolean,
    "lPagoCredito" boolean,
    "lOpeDeposito" boolean,
    "lOpeCancelacion" boolean,
    "lOpePagSep" boolean,
    "lOpeIndemn" boolean,
    "lOrdenCompra" boolean,
    "lOpeBanco" boolean,
    "nOrdenOpeRetiro" text,
    "lAhoProgramado" boolean,
    PRIMARY KEY ("idTipoPago")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_motivo_operacion" (
    "idMotivoOperacion" int,
    "idTipoOperacion" int,
    "cMotivoOperacion" text,
    "lVigente" boolean,
    "lReqAutorizacion" boolean,
    PRIMARY KEY ("idMotivoOperacion")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_tipo_limite" (
    "idTipoLimite" int,
    "cNombre" text,
    "lVigente" boolean,
    PRIMARY KEY ("idTipoLimite")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_caracteristica_bloq" (
    "idCaracteristicaBloq" int,
    "cDescripcion" text,
    "lIsReqMonto" boolean,
    "lEstado" boolean,
    "idTipoOperacion" int,
    PRIMARY KEY ("idCaracteristicaBloq")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_tipo_cuenta" (
    "idTipoCuenta" int,
    "cTipoCuenta" text,
    "lVigente" boolean,
    PRIMARY KEY ("idTipoCuenta")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_ambito_transferencia" (
    "idAmbitoTransferencia" int,
    "cAmbitoTransferencia" text,
    "cNombreCorto" text,
    "lVigente" boolean,
    PRIMARY KEY ("idAmbitoTransferencia")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_clasificacion_transferencia" (
    "idClasificacionTransferencia" int,
    "cTipoEntidad" text,
    PRIMARY KEY ("idClasificacionTransferencia")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_detalle_ope_rechazado" (
    "idDetalleOpeRechazado" int,
    "nMontoOperacion" decimal,
    "idTipoOperacion" int,
    "idEntidadOperacion" int,
    "nComision" decimal,
    "idCargoComision" int,
    "idAmbitoTransferencia" int,
    "idConceptoComision" int,
    "idModalidadConsultaCCI" int,
    "cNumeroCCIOrigen" text,
    "cNumeroCCIDestino" text,
    "cCodigoOperacion" text,
    "cCodigoOperacionOrden" text,
    "cNumeroCelDestino" text,
    "lRechazadoCCE" boolean,
    "cCodigoRechazado" text,
    "dFechaHoraOpe" timestamp,
    "idCanal" int,
    "cDocumentoID" text,
    "idMoneda" int,
    "dFechaHoraCCEOrden" timestamp,
    "lVigente" boolean,
    PRIMARY KEY ("idDetalleOpeRechazado")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_detalle_transf_kardex" (
    "idDetalleTransfKardex" int,
    "idKardex" int,
    "nMontoOperacion" decimal,
    "idTipoOperacion" int,
    "idEntidadOperacion" int,
    "nComision" decimal,
    "idCargoComision" int,
    "idAmbitoTransferencia" int,
    "idConceptoComision" int,
    "idModalidadConsultaCCI" int,
    "cNumeroCCI" text,
    "cCodigoOperacion" text,
    "cCodigoOperacionOrden" text,
    "cCodigoInstruccion" text,
    "cNumeroCelDestino" text,
    "dFechaHoraOpe" timestamp,
    "dFechaHoraCCEOrden" timestamp,
    "lVigente" boolean,
    PRIMARY KEY ("idDetalleTransfKardex")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_tipo_transaccion" (
    "idTipoTransaccion" int,
    "cTipoTransaccion" text,
    "lVigente" boolean,
    PRIMARY KEY ("idTipoTransaccion")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_tipo_transferencia" (
    "idTiTransfer" int,
    "cDescripcion" text,
    "lEstado" boolean,
    "idTipoOpeVinc" int,
    PRIMARY KEY ("idTiTransfer")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_saldo_deposito" (
    "dFecha" date,
    "idCuenta" int,
    "idCli" int,
    "idProducto" int,
    "idEstado" int,
    "IdMoneda" int,
    "nSaldo" decimal,
    "idAgencia" int,
    "idCondicionContable" int,
    "cRucEmpleador" text,
    "lGarantia" boolean,
    "idPromotor" int,
    "nMonApertura" decimal,
    "nMonDepositos" decimal,
    "nMonRetiros" decimal,
    "nMonCancelacion" decimal,
    "nMonCapitalizaInt" decimal,
    "nMonRetiroInteres" decimal,
    "nMonCargos" decimal,
    "nMonIntDevengado" decimal,
    "nMonBloqGarantia" decimal,
    "nMonBloqJudicial" decimal,
    "nMonBloqSunat" decimal,
    "dFechaApertura" date,
    "dFechaUltRenovacion" date,
    "nTasa" decimal,
    "nInteresPag" decimal,
    "nInteresGen" decimal,
    "nMontoApertura" decimal,
    "nNumeroFirmas" int,
    "lInactiva" boolean,
    "lIsAfectoITF" boolean,
    "lIsCtaOrdPago" boolean,
    "dFecCapitalizacion" date,
    "idTasa" int,
    "idCaracteristica" int,
    "idTipoEnvioEstCta" int,
    "cDireccionEnvioEstCta" text,
    "nNroImpresionExtCta" int,
    "idTipoPersona" int,
    "idSaldo" bigint,
    PRIMARY KEY ("idSaldo")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_det_depos_cts" (
    "idKardex" int,
    "idCuenta" int,
    "nMontoIntangible" decimal,
    "nMontoDisp" decimal,
    "idTipoOperacion" int,
    "lVigente" boolean,
    PRIMARY KEY ("idKardex")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_reg_documento_cts" (
    "idRegDoc" int,
    "idCuenta" int,
    "idDoc" int,
    "cDescripcion" text,
    "bEnviado" boolean,
    "bFirmado" boolean,
    "lVigente" boolean,
    PRIMARY KEY ("idRegDoc")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_deposito_prg" (
    "idCuenta" int,
    "nNroDepos" int,
    "nNumDepPag" int,
    "nDiasAtrazo" int,
    "nMontoCuota" decimal,
    "nMonAdelantado" decimal,
    "nMonTotProg" decimal,
    "idPlanPagos" int,
    "dFechaCulminacion" date,
    "lVigente" boolean,
    "idMotivo" int,
    "lDebito" boolean,
    "nMesGracia" int,
    "lBeneficio" boolean,
    "lCmpContrato" boolean,
    "idCtaDebitoPrg" int,
    "idBonificacion" int,
    "idEstadoCta" int,
    PRIMARY KEY ("idCuenta")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_bloqueo_cuenta" (
    "idBloCta" int,
    "idCuenta" int,
    "idBloqueo" int,
    "idCaracteristicaBloq" int,
    "nMonBloqueo" decimal,
    "cDesMotivo" text,
    "lBloqueado" boolean,
    "idUsuBloq" int,
    "dFechaBloq" date,
    "idUsuDesbloq" int,
    "dFechaDesbloq" date,
    "idTipoSolicitante" int,
    "cNombreSolicitante" text,
    "idTipoSolDesbloqueo" int,
    "cNombreSolDesbloqueo" text,
    "lvigente" boolean,
    "dFechaDocBloqueo" date,
    "dFecHoraBloq" timestamp,
    "cMotivoDesbloq" text,
    "dFecHoraDesbloq" timestamp,
    "idSolicitud" int,
    PRIMARY KEY ("idBloCta")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_parametro_prod" (
    "IdProducto" int,
    "IdMoneda" int,
    "lIsInactiva" boolean,
    "nPlaInactiva" int,
    "nPlaCancInac" int,
    "nMonMinApe" decimal,
    "nMonMinSalDis" decimal,
    "nMonMinOpe" decimal,
    "nPlaMinProd" int,
    "nPlaMaxProd" int,
    "lIsPlazoProd" boolean,
    "lIsRenovProd" boolean,
    "lIsRequeCert" boolean,
    "lIsAfectoITF" boolean,
    "lIsTrabFinan" boolean,
    "lIsReqPagInt" boolean,
    "lIsDefTipPagInt" boolean,
    "lIsReqEmpleador" boolean,
    "lIsProdTransf" boolean,
    "lIsProdCTS" boolean,
    "lIsDepAhoProg" boolean,
    "lVigente" boolean,
    "nTipCalInt" int,
    "nPeriodoCap" int,
    "lIsOpeRetiro" boolean,
    "lIsOpeDeposito" boolean,
    "lIsCtaCteRel" boolean,
    "idProdTasCancel" int,
    "lIsCtaOrdPago" boolean,
    "idClasifProdDep" int,
    "lIsDepMenEdad" boolean,
    "idTipoCapDep" int,
    "lIsGarantia" boolean,
    "lActualizaTasaRen" boolean,
    "idProdTasaRenov" int,
    "nMonMaxApe" decimal,
    "nPlaMinProdMeses" int,
    "nPlaMaxProdMeses" int,
    PRIMARY KEY (("IdProducto", "IdMoneda"))
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_tipo_tasa" (
    "idTipoTasa" int,
    "cTipoTasa" text,
    "lVigente" boolean,
    "idModulo" int,
    PRIMARY KEY ("idTipoTasa")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_producto" (
    "IdProducto" int,
    "cCuentaContable" text,
    "cProducto" text,
    "IdProductoPadre" int,
    "lVigente" boolean,
    "idModulo" int,
    "cCodSBS" text,
    "dVigenciaInicio" date,
    "dVigenciaFin" date,
    "lConfigurable" boolean,
    "idFuenteCalcMora" int,
    "cProductoCorto" text,
    "lAhorroWeb" boolean,
    "idGrupoCamp" int,
    "lTasaClasifInterna" boolean,
    "cTipoOperacionCPE" text,
    PRIMARY KEY ("IdProducto")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_tipo_transferencia_limites" (
    "idTipoTransferencia" int,
    "cTipoTransferencia" text,
    "idClasificacionTransferencia" int,
    PRIMARY KEY ("idTipoTransferencia")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_deposito" (
    "idCuenta" int,
    "idEstado" int,
    "idProducto" int,
    "idTipoCuenta" int,
    "idTipoTasa" int,
    "idTasa" int,
    "nMonTasa" decimal,
    "idMoneda" int,
    "idAgencia" int,
    "idUsuario" int,
    "idUsuarioqBlo" int,
    "nPlazoCta" int,
    "nMontoDeposito" decimal,
    "nInteresGen" decimal,
    "nInteresPag" decimal,
    "nMonIntDev" decimal,
    "nSaldoDis" decimal,
    "nSaldoCnt" decimal,
    "nMonIntTot" decimal,
    "dFechaApertura" date,
    "dFechaCancelacion" date,
    "nNumeroFirmas" int,
    "idCaracteristica" int,
    "idUsuAsig" int,
    "lisBloqCta" boolean,
    "nMonTotBloq" decimal,
    "nTasaCancelacion" decimal,
    "dFecUltMov" timestamp,
    "cRucEmpleador" text,
    "lInactiva" boolean,
    "lIsAfectoITF" boolean,
    "lIsCtaOrdPago" boolean,
    "idRenovacion" int,
    "idPagoInt" int,
    "idTipoPersona" int,
    "dFecCapitalizacion" date,
    "cNroCuenta" text,
    "idCuentaRel" int,
    "nSaldoMinimo" decimal,
    "nNumeCertificado" int,
    "lIndGarantia" boolean,
    "dFechaAperturaIni" date,
    "dFechaAtencion" timestamp,
    "idUsuAtiende" int,
    "nDiaPagoInt" int,
    "cDescripcionCta" text,
    "idTipoEnvioEstCta" int,
    "cDireccionEnvioEstCta" text,
    "idOrigenFondo" int,
    "idObjetoAho" int,
    "cNumeroCCI" text,
    "nMontoApertura" decimal,
    "nNroImpresionExtCta" int,
    "nMontoApeIni" decimal,
    "dFechaDevengado" date,
    "nIntDevTmp" decimal,
    "idCuentaCreditoApertura" int,
    "lCuentaDepositoAutomatico" int,
    "idCanalRegistro" int,
    "idMotivoAnulacionDep" int,
    "nDiasExecPenCuenta" int,
    "idModoDesembolsoCli" int,
    "idSolicitudCre" int,
    "lAlertaSms" boolean,
    "nRevisadoChecklist" int,
    "idTipoFirma" int,
    "nPlazoMeses" int,
    PRIMARY KEY ("idCuenta")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_kardex" (
    "IdKardex" int,
    "dFechaOpe" date,
    "dFecHoraOpe" timestamp,
    "idProducto" int,
    "idCanal" int,
    "idMoneda" int,
    "IdUsuario" int,
    "idCuenta" int,
    "idTipoOperacion" int,
    "idEstadoKardex" int,
    "IdKardexExtorno" int,
    "nMontoOperacion" decimal,
    "nMontoCapital" decimal,
    "nMontoInteres" decimal,
    "nMontoMora" decimal,
    "nMontoOtros" decimal,
    "nITFNormal" decimal,
    "idTipoIngresoEgreso" int,
    "IdMotivoExt" int,
    "cObservaExt" text,
    "idAgeSaldo" int,
    "idAgeOpera" int,
    "idModulo" int,
    "idTipoAfeCaj" int,
    "nAtrasoPago" smallint,
    "idTipoPago" int,
    "idTipoPersona" int,
    "idCliAfeITF" int,
    "cNroDNI" text,
    "cNomCliente" text,
    "cDireccion" text,
    "idKardexRelacionado" int,
    "cObservacion" text,
    "nMontoIntComp" decimal,
    "idMotivoOperacion" int,
    "idEstablecimiento" int,
    "idTipoTrxOpe" int,
    "dFechaValor" date,
    PRIMARY KEY ("IdKardex")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_limite_transferencia" (
    "idLimiteTransferencia" int,
    "idModulo" int,
    "idModalidadConsultaCCI" int,
    "idCanal" int,
    "idProducto" int,
    "idTipoOperacion" int,
    "idMoneda" int,
    "idDirectorioCCE" int,
    "idTipoLimite" int,
    "idTipoEntidadTrans" int,
    "idPropiedadTitularTrans" int,
    "nMontoMinimo" decimal,
    "nMontoMaximo" decimal,
    "lLimitePorDefecto" boolean,
    "lVigente" boolean,
    "dFechaInicio" date,
    "dFechaUpdate" date,
    PRIMARY KEY ("idLimiteTransferencia")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_deposito_cts" (
    "idCuenta" int,
    "nMontoIntangible" decimal,
    "nMontoDisp" decimal,
    "idEntidad" int,
    "idEmpleador" int,
    PRIMARY KEY ("idCuenta")
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_limite_transferencia_concepto" (
    "IdLimiteTransferencia" int,
    "IdConceptoComision" int,
    PRIMARY KEY (("IdLimiteTransferencia", "IdConceptoComision"))
);

CREATE TABLE IF NOT EXISTS "db_savings"."si_fin_outbox_transaccion" (
    "idOutbox" bigint,
    "idTrxIdempotency" uuid,
    "idCuenta" int,
    "nMonto" decimal,
    "lEsRetiro" boolean,
    "lEstaCerrado" boolean,
    "dFechaRegistro" timestamp,
    "dfechaCierre" timestamp,
    PRIMARY KEY ("idOutbox")
);
