using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_canal", ExplicitColumns = true)]
internal sealed class SiFinCanal
{
    [PartitionKey(0)]
[Column("idCanal")]
public int IdCanal { get; init; }

    [Column("cCanal")]
public string? CCanal { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }

    [Column("cCodSBS")]
public string? CCodSbs { get; init; }
}
