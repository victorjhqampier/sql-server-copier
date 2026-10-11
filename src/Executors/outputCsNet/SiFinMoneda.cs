using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_moneda", ExplicitColumns = true)]
internal sealed class SiFinMoneda
{
    [PartitionKey(0)]
[Column("idMoneda")]
public int idMoneda { get; init; }

    [Column("cMoneda")]
public string? cMoneda { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }

    [Column("CodigoISO")]
public string? CodigoISO { get; init; }

    [Column("cCodSBS")]
public string? cCodSBS { get; init; }

    [Column("cSimbolo")]
public string? cSimbolo { get; init; }

    [Column("cContable")]
public string? cContable { get; init; }

    [Column("lComision")]
public bool? lComision { get; init; }

    [Column("cCodCCE")]
public string? cCodCCE { get; init; }
}
