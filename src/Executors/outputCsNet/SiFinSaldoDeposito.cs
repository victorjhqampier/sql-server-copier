using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_saldo_deposito", ExplicitColumns = true)]
internal sealed class SiFinSaldoDeposito
{
    [Column("dFecha")]
public Cassandra.LocalDate? dFecha { get; init; }

    [Column("idCuenta")]
public int idCuenta { get; init; }

    [Column("idCli")]
public int? idCli { get; init; }

    [Column("idProducto")]
public int? idProducto { get; init; }

    [Column("idEstado")]
public int? idEstado { get; init; }

    [Column("IdMoneda")]
public int? IdMoneda { get; init; }

    [Column("nSaldo")]
public decimal? nSaldo { get; init; }

    [Column("idAgencia")]
public int? idAgencia { get; init; }

    [Column("idCondicionContable")]
public int? idCondicionContable { get; init; }

    [Column("cRucEmpleador")]
public string? cRucEmpleador { get; init; }

    [Column("lGarantia")]
public bool? lGarantia { get; init; }

    [Column("idPromotor")]
public int? idPromotor { get; init; }

    [Column("nMonApertura")]
public decimal? nMonApertura { get; init; }

    [Column("nMonDepositos")]
public decimal? nMonDepositos { get; init; }

    [Column("nMonRetiros")]
public decimal? nMonRetiros { get; init; }

    [Column("nMonCancelacion")]
public decimal? nMonCancelacion { get; init; }

    [Column("nMonCapitalizaInt")]
public decimal? nMonCapitalizaInt { get; init; }

    [Column("nMonRetiroInteres")]
public decimal? nMonRetiroInteres { get; init; }

    [Column("nMonCargos")]
public decimal? nMonCargos { get; init; }

    [Column("nMonIntDevengado")]
public decimal? nMonIntDevengado { get; init; }

    [Column("nMonBloqGarantia")]
public decimal? nMonBloqGarantia { get; init; }

    [Column("nMonBloqJudicial")]
public decimal? nMonBloqJudicial { get; init; }

    [Column("nMonBloqSunat")]
public decimal? nMonBloqSunat { get; init; }

    [Column("dFechaApertura")]
public Cassandra.LocalDate? dFechaApertura { get; init; }

    [Column("dFechaUltRenovacion")]
public Cassandra.LocalDate? dFechaUltRenovacion { get; init; }

    [Column("nTasa")]
public decimal? nTasa { get; init; }

    [Column("nInteresPag")]
public decimal? nInteresPag { get; init; }

    [Column("nInteresGen")]
public decimal? nInteresGen { get; init; }

    [Column("nMontoApertura")]
public decimal? nMontoApertura { get; init; }

    [Column("nNumeroFirmas")]
public int? nNumeroFirmas { get; init; }

    [Column("lInactiva")]
public bool? lInactiva { get; init; }

    [Column("lIsAfectoITF")]
public bool? lIsAfectoITF { get; init; }

    [Column("lIsCtaOrdPago")]
public bool? lIsCtaOrdPago { get; init; }

    [Column("dFecCapitalizacion")]
public Cassandra.LocalDate? dFecCapitalizacion { get; init; }

    [Column("idTasa")]
public int? idTasa { get; init; }

    [Column("idCaracteristica")]
public int? idCaracteristica { get; init; }

    [Column("idTipoEnvioEstCta")]
public int? idTipoEnvioEstCta { get; init; }

    [Column("cDireccionEnvioEstCta")]
public string? cDireccionEnvioEstCta { get; init; }

    [Column("nNroImpresionExtCta")]
public int? nNroImpresionExtCta { get; init; }

    [Column("idTipoPersona")]
public int idTipoPersona { get; init; }

    [PartitionKey(0)]
[Column("idSaldo")]
public long idSaldo { get; init; }
}
