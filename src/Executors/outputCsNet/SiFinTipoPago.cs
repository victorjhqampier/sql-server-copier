using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_pago", ExplicitColumns = true)]
internal sealed class SiFinTipoPago
{
    [PartitionKey(0)]
[Column("idTipoPago")]
public int IdTipoPago { get; init; }

    [Column("cDesTipoPago")]
public string CDesTipoPago { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }

    [Column("lOpeRetiro")]
public bool? LOpeRetiro { get; init; }

    [Column("lOpeApertura")]
public bool? LOpeApertura { get; init; }

    [Column("cCodSBS")]
public string? CCodSbs { get; init; }

    [Column("lOpePagoComPago")]
public bool? LOpePagoComPago { get; init; }

    [Column("lPagoCredito")]
public bool? LPagoCredito { get; init; }

    [Column("lOpeDeposito")]
public bool? LOpeDeposito { get; init; }

    [Column("lOpeCancelacion")]
public bool? LOpeCancelacion { get; init; }

    [Column("lOpePagSep")]
public bool? LOpePagSep { get; init; }

    [Column("lOpeIndemn")]
public bool? LOpeIndemn { get; init; }

    [Column("lOrdenCompra")]
public bool? LOrdenCompra { get; init; }

    [Column("lOpeBanco")]
public bool? LOpeBanco { get; init; }

    [Column("nOrdenOpeRetiro")]
public string? NOrdenOpeRetiro { get; init; }

    [Column("lAhoProgramado")]
public bool? LAhoProgramado { get; init; }
}
