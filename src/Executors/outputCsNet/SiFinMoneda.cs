using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_moneda", ExplicitColumns = true)]
internal sealed class SiFinMoneda
{
    [PartitionKey(0)]
[Column("idMoneda")]
public int IdMoneda { get; init; }

    [Column("cMoneda")]
public string? CMoneda { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }

    [Column("CodigoISO")]
public string? CodigoIso { get; init; }

    [Column("cCodSBS")]
public string? CCodSbs { get; init; }

    [Column("cSimbolo")]
public string? CSimbolo { get; init; }

    [Column("cContable")]
public string? CContable { get; init; }

    [Column("lComision")]
public bool? LComision { get; init; }

    [Column("cCodCCE")]
public string? CCodCce { get; init; }
}
