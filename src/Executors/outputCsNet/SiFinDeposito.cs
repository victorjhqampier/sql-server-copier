using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_deposito", ExplicitColumns = true)]
internal sealed class SiFinDeposito
{
    [PartitionKey(0)]
[Column("idCuenta")]
public int idCuenta { get; init; }

    [Column("idEstado")]
public int? idEstado { get; init; }

    [Column("idProducto")]
public int? idProducto { get; init; }

    [Column("idTipoCuenta")]
public int? idTipoCuenta { get; init; }

    [Column("idTipoTasa")]
public int? idTipoTasa { get; init; }

    [Column("idTasa")]
public int? idTasa { get; init; }

    [Column("nMonTasa")]
public decimal? nMonTasa { get; init; }

    [Column("idMoneda")]
public int? idMoneda { get; init; }

    [Column("idAgencia")]
public int? idAgencia { get; init; }

    [Column("idUsuario")]
public int? idUsuario { get; init; }

    [Column("idUsuarioqBlo")]
public int? idUsuarioqBlo { get; init; }

    [Column("nPlazoCta")]
public int? nPlazoCta { get; init; }

    [Column("nMontoDeposito")]
public decimal? nMontoDeposito { get; init; }

    [Column("nInteresGen")]
public decimal? nInteresGen { get; init; }

    [Column("nInteresPag")]
public decimal? nInteresPag { get; init; }

    [Column("nMonIntDev")]
public decimal? nMonIntDev { get; init; }

    [Column("nSaldoDis")]
public decimal? nSaldoDis { get; init; }

    [Column("nSaldoCnt")]
public decimal? nSaldoCnt { get; init; }

    [Column("nMonIntTot")]
public decimal? nMonIntTot { get; init; }

    [Column("dFechaApertura")]
public Cassandra.LocalDate? dFechaApertura { get; init; }

    [Column("dFechaCancelacion")]
public Cassandra.LocalDate? dFechaCancelacion { get; init; }

    [Column("nNumeroFirmas")]
public int? nNumeroFirmas { get; init; }

    [Column("idCaracteristica")]
public int? idCaracteristica { get; init; }

    [Column("idUsuAsig")]
public int? idUsuAsig { get; init; }

    [Column("lisBloqCta")]
public bool? lisBloqCta { get; init; }

    [Column("nMonTotBloq")]
public decimal? nMonTotBloq { get; init; }

    [Column("nTasaCancelacion")]
public decimal? nTasaCancelacion { get; init; }

    [Column("dFecUltMov")]
public System.DateTime? dFecUltMov { get; init; }

    [Column("cRucEmpleador")]
public string? cRucEmpleador { get; init; }

    [Column("lInactiva")]
public bool? lInactiva { get; init; }

    [Column("lIsAfectoITF")]
public bool? lIsAfectoITF { get; init; }

    [Column("lIsCtaOrdPago")]
public bool? lIsCtaOrdPago { get; init; }

    [Column("idRenovacion")]
public int? idRenovacion { get; init; }

    [Column("idPagoInt")]
public int? idPagoInt { get; init; }

    [Column("idTipoPersona")]
public int? idTipoPersona { get; init; }

    [Column("dFecCapitalizacion")]
public Cassandra.LocalDate? dFecCapitalizacion { get; init; }

    [Column("cNroCuenta")]
public string? cNroCuenta { get; init; }

    [Column("idCuentaRel")]
public int? idCuentaRel { get; init; }

    [Column("nSaldoMinimo")]
public decimal? nSaldoMinimo { get; init; }

    [Column("nNumeCertificado")]
public int? nNumeCertificado { get; init; }

    [Column("lIndGarantia")]
public bool? lIndGarantia { get; init; }

    [Column("dFechaAperturaIni")]
public Cassandra.LocalDate? dFechaAperturaIni { get; init; }

    [Column("dFechaAtencion")]
public System.DateTime? dFechaAtencion { get; init; }

    [Column("idUsuAtiende")]
public int? idUsuAtiende { get; init; }

    [Column("nDiaPagoInt")]
public int? nDiaPagoInt { get; init; }

    [Column("cDescripcionCta")]
public string? cDescripcionCta { get; init; }

    [Column("idTipoEnvioEstCta")]
public int? idTipoEnvioEstCta { get; init; }

    [Column("cDireccionEnvioEstCta")]
public string? cDireccionEnvioEstCta { get; init; }

    [Column("idOrigenFondo")]
public int? idOrigenFondo { get; init; }

    [Column("idObjetoAho")]
public int? idObjetoAho { get; init; }

    [Column("cNumeroCCI")]
public string? cNumeroCCI { get; init; }

    [Column("nMontoApertura")]
public decimal? nMontoApertura { get; init; }

    [Column("nNroImpresionExtCta")]
public int? nNroImpresionExtCta { get; init; }

    [Column("nMontoApeIni")]
public decimal? nMontoApeIni { get; init; }

    [Column("dFechaDevengado")]
public Cassandra.LocalDate? dFechaDevengado { get; init; }

    [Column("nIntDevTmp")]
public decimal nIntDevTmp { get; init; }

    [Column("idCuentaCreditoApertura")]
public int? idCuentaCreditoApertura { get; init; }

    [Column("lCuentaDepositoAutomatico")]
public int? lCuentaDepositoAutomatico { get; init; }

    [Column("idCanalRegistro")]
public int? idCanalRegistro { get; init; }

    [Column("idMotivoAnulacionDep")]
public int? idMotivoAnulacionDep { get; init; }

    [Column("nDiasExecPenCuenta")]
public int? nDiasExecPenCuenta { get; init; }

    [Column("idModoDesembolsoCli")]
public int? idModoDesembolsoCli { get; init; }

    [Column("idSolicitudCre")]
public int? idSolicitudCre { get; init; }

    [Column("lAlertaSms")]
public bool? lAlertaSms { get; init; }

    [Column("nRevisadoChecklist")]
public int nRevisadoChecklist { get; init; }

    [Column("idTipoFirma")]
public int idTipoFirma { get; init; }

    [Column("nPlazoMeses")]
public int? nPlazoMeses { get; init; }
}
