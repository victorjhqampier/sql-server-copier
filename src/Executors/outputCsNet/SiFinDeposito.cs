using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_deposito", ExplicitColumns = true)]
internal sealed class SiFinDeposito
{
    [PartitionKey(0)]
[Column("idCuenta")]
public int IdCuenta { get; init; }

    [Column("idEstado")]
public int? IdEstado { get; init; }

    [Column("idProducto")]
public int? IdProducto { get; init; }

    [Column("idTipoCuenta")]
public int? IdTipoCuenta { get; init; }

    [Column("idTipoTasa")]
public int? IdTipoTasa { get; init; }

    [Column("idTasa")]
public int? IdTasa { get; init; }

    [Column("nMonTasa")]
public decimal? NMonTasa { get; init; }

    [Column("idMoneda")]
public int? IdMoneda { get; init; }

    [Column("idAgencia")]
public int? IdAgencia { get; init; }

    [Column("idUsuario")]
public int? IdUsuario { get; init; }

    [Column("idUsuarioqBlo")]
public int? IdUsuarioqBlo { get; init; }

    [Column("nPlazoCta")]
public int? NPlazoCta { get; init; }

    [Column("nMontoDeposito")]
public decimal? NMontoDeposito { get; init; }

    [Column("nInteresGen")]
public decimal? NInteresGen { get; init; }

    [Column("nInteresPag")]
public decimal? NInteresPag { get; init; }

    [Column("nMonIntDev")]
public decimal? NMonIntDev { get; init; }

    [Column("nSaldoDis")]
public decimal? NSaldoDis { get; init; }

    [Column("nSaldoCnt")]
public decimal? NSaldoCnt { get; init; }

    [Column("nMonIntTot")]
public decimal? NMonIntTot { get; init; }

    [Column("dFechaApertura")]
public Cassandra.LocalDate? DFechaApertura { get; init; }

    [Column("dFechaCancelacion")]
public Cassandra.LocalDate? DFechaCancelacion { get; init; }

    [Column("nNumeroFirmas")]
public int? NNumeroFirmas { get; init; }

    [Column("idCaracteristica")]
public int? IdCaracteristica { get; init; }

    [Column("idUsuAsig")]
public int? IdUsuAsig { get; init; }

    [Column("lisBloqCta")]
public bool? LisBloqCta { get; init; }

    [Column("nMonTotBloq")]
public decimal? NMonTotBloq { get; init; }

    [Column("nTasaCancelacion")]
public decimal? NTasaCancelacion { get; init; }

    [Column("dFecUltMov")]
public System.DateTime? DFecUltMov { get; init; }

    [Column("cRucEmpleador")]
public string? CRucEmpleador { get; init; }

    [Column("lInactiva")]
public bool? LInactiva { get; init; }

    [Column("lIsAfectoITF")]
public bool? LIsAfectoItf { get; init; }

    [Column("lIsCtaOrdPago")]
public bool? LIsCtaOrdPago { get; init; }

    [Column("idRenovacion")]
public int? IdRenovacion { get; init; }

    [Column("idPagoInt")]
public int? IdPagoInt { get; init; }

    [Column("idTipoPersona")]
public int? IdTipoPersona { get; init; }

    [Column("dFecCapitalizacion")]
public Cassandra.LocalDate? DFecCapitalizacion { get; init; }

    [Column("cNroCuenta")]
public string? CNroCuenta { get; init; }

    [Column("idCuentaRel")]
public int? IdCuentaRel { get; init; }

    [Column("nSaldoMinimo")]
public decimal? NSaldoMinimo { get; init; }

    [Column("nNumeCertificado")]
public int? NNumeCertificado { get; init; }

    [Column("lIndGarantia")]
public bool? LIndGarantia { get; init; }

    [Column("dFechaAperturaIni")]
public Cassandra.LocalDate? DFechaAperturaIni { get; init; }

    [Column("dFechaAtencion")]
public System.DateTime? DFechaAtencion { get; init; }

    [Column("idUsuAtiende")]
public int? IdUsuAtiende { get; init; }

    [Column("nDiaPagoInt")]
public int? NDiaPagoInt { get; init; }

    [Column("cDescripcionCta")]
public string? CDescripcionCta { get; init; }

    [Column("idTipoEnvioEstCta")]
public int? IdTipoEnvioEstCta { get; init; }

    [Column("cDireccionEnvioEstCta")]
public string? CDireccionEnvioEstCta { get; init; }

    [Column("idOrigenFondo")]
public int? IdOrigenFondo { get; init; }

    [Column("idObjetoAho")]
public int? IdObjetoAho { get; init; }

    [Column("cNumeroCCI")]
public string? CNumeroCci { get; init; }

    [Column("nMontoApertura")]
public decimal? NMontoApertura { get; init; }

    [Column("nNroImpresionExtCta")]
public int? NNroImpresionExtCta { get; init; }

    [Column("nMontoApeIni")]
public decimal? NMontoApeIni { get; init; }

    [Column("dFechaDevengado")]
public Cassandra.LocalDate? DFechaDevengado { get; init; }

    [Column("nIntDevTmp")]
public decimal NIntDevTmp { get; init; }

    [Column("idCuentaCreditoApertura")]
public int? IdCuentaCreditoApertura { get; init; }

    [Column("lCuentaDepositoAutomatico")]
public int? LCuentaDepositoAutomatico { get; init; }

    [Column("idCanalRegistro")]
public int? IdCanalRegistro { get; init; }

    [Column("idMotivoAnulacionDep")]
public int? IdMotivoAnulacionDep { get; init; }

    [Column("nDiasExecPenCuenta")]
public int? NDiasExecPenCuenta { get; init; }

    [Column("idModoDesembolsoCli")]
public int? IdModoDesembolsoCli { get; init; }

    [Column("idSolicitudCre")]
public int? IdSolicitudCre { get; init; }

    [Column("lAlertaSms")]
public bool? LAlertaSms { get; init; }

    [Column("nRevisadoChecklist")]
public int NRevisadoChecklist { get; init; }

    [Column("idTipoFirma")]
public int IdTipoFirma { get; init; }

    [Column("nPlazoMeses")]
public int? NPlazoMeses { get; init; }
}
