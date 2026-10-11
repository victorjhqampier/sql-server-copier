using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_canal", ExplicitColumns = true)]
internal sealed class SiFinCanal
{
    [PartitionKey(0)]
[Column("idCanal")]
public int idCanal { get; init; }

    [Column("cCanal")]
public string? cCanal { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }

    [Column("cCodSBS")]
public string? cCodSBS { get; init; }
}
