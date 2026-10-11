using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_canal_cce", ExplicitColumns = true)]
internal sealed class SiFinCanalCce
{
    [PartitionKey(0)]
[Column("idCanalCCE")]
public int idCanalCCE { get; init; }

    [Column("cCanalCCE")]
public string? cCanalCCE { get; init; }

    [Column("cCodigo")]
public string? cCodigo { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
