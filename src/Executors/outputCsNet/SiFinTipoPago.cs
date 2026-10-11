using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_pago", ExplicitColumns = true)]
internal sealed class SiFinTipoPago
{
    [PartitionKey(0)]
[Column("idTipoPago")]
public int idTipoPago { get; init; }

    [Column("cDesTipoPago")]
public string cDesTipoPago { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }

    [Column("lOpeRetiro")]
public bool? lOpeRetiro { get; init; }

    [Column("lOpeApertura")]
public bool? lOpeApertura { get; init; }

    [Column("cCodSBS")]
public string? cCodSBS { get; init; }

    [Column("lOpePagoComPago")]
public bool? lOpePagoComPago { get; init; }

    [Column("lPagoCredito")]
public bool? lPagoCredito { get; init; }

    [Column("lOpeDeposito")]
public bool? lOpeDeposito { get; init; }

    [Column("lOpeCancelacion")]
public bool? lOpeCancelacion { get; init; }

    [Column("lOpePagSep")]
public bool? lOpePagSep { get; init; }

    [Column("lOpeIndemn")]
public bool? lOpeIndemn { get; init; }

    [Column("lOrdenCompra")]
public bool? lOrdenCompra { get; init; }

    [Column("lOpeBanco")]
public bool? lOpeBanco { get; init; }

    [Column("nOrdenOpeRetiro")]
public string? nOrdenOpeRetiro { get; init; }

    [Column("lAhoProgramado")]
public bool? lAhoProgramado { get; init; }
}
