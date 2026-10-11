using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_saldo_deposito", ExplicitColumns = true)]
internal sealed class SiFinSaldoDeposito
{
    [Column("dFecha")]
public Cassandra.LocalDate? DFecha { get; init; }

    [Column("idCuenta")]
public int IdCuenta { get; init; }

    [Column("idCli")]
public int? IdCli { get; init; }

    [Column("idProducto")]
public int? IdProducto { get; init; }

    [Column("idEstado")]
public int? IdEstado { get; init; }

    [Column("IdMoneda")]
public int? IdMoneda { get; init; }

    [Column("nSaldo")]
public decimal? NSaldo { get; init; }

    [Column("idAgencia")]
public int? IdAgencia { get; init; }

    [Column("idCondicionContable")]
public int? IdCondicionContable { get; init; }

    [Column("cRucEmpleador")]
public string? CRucEmpleador { get; init; }

    [Column("lGarantia")]
public bool? LGarantia { get; init; }

    [Column("idPromotor")]
public int? IdPromotor { get; init; }

    [Column("nMonApertura")]
public decimal? NMonApertura { get; init; }

    [Column("nMonDepositos")]
public decimal? NMonDepositos { get; init; }

    [Column("nMonRetiros")]
public decimal? NMonRetiros { get; init; }

    [Column("nMonCancelacion")]
public decimal? NMonCancelacion { get; init; }

    [Column("nMonCapitalizaInt")]
public decimal? NMonCapitalizaInt { get; init; }

    [Column("nMonRetiroInteres")]
public decimal? NMonRetiroInteres { get; init; }

    [Column("nMonCargos")]
public decimal? NMonCargos { get; init; }

    [Column("nMonIntDevengado")]
public decimal? NMonIntDevengado { get; init; }

    [Column("nMonBloqGarantia")]
public decimal? NMonBloqGarantia { get; init; }

    [Column("nMonBloqJudicial")]
public decimal? NMonBloqJudicial { get; init; }

    [Column("nMonBloqSunat")]
public decimal? NMonBloqSunat { get; init; }

    [Column("dFechaApertura")]
public Cassandra.LocalDate? DFechaApertura { get; init; }

    [Column("dFechaUltRenovacion")]
public Cassandra.LocalDate? DFechaUltRenovacion { get; init; }

    [Column("nTasa")]
public decimal? NTasa { get; init; }

    [Column("nInteresPag")]
public decimal? NInteresPag { get; init; }

    [Column("nInteresGen")]
public decimal? NInteresGen { get; init; }

    [Column("nMontoApertura")]
public decimal? NMontoApertura { get; init; }

    [Column("nNumeroFirmas")]
public int? NNumeroFirmas { get; init; }

    [Column("lInactiva")]
public bool? LInactiva { get; init; }

    [Column("lIsAfectoITF")]
public bool? LIsAfectoItf { get; init; }

    [Column("lIsCtaOrdPago")]
public bool? LIsCtaOrdPago { get; init; }

    [Column("dFecCapitalizacion")]
public Cassandra.LocalDate? DFecCapitalizacion { get; init; }

    [Column("idTasa")]
public int? IdTasa { get; init; }

    [Column("idCaracteristica")]
public int? IdCaracteristica { get; init; }

    [Column("idTipoEnvioEstCta")]
public int? IdTipoEnvioEstCta { get; init; }

    [Column("cDireccionEnvioEstCta")]
public string? CDireccionEnvioEstCta { get; init; }

    [Column("nNroImpresionExtCta")]
public int? NNroImpresionExtCta { get; init; }

    [Column("idTipoPersona")]
public int IdTipoPersona { get; init; }

    [PartitionKey(0)]
[Column("idSaldo")]
public long IdSaldo { get; init; }
}
