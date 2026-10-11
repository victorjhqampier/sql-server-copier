using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_canal_cce", ExplicitColumns = true)]
internal sealed class SiFinCanalCce
{
    [PartitionKey(0)]
[Column("idCanalCCE")]
public int IdCanalCce { get; init; }

    [Column("cCanalCCE")]
public string? CCanalCce { get; init; }

    [Column("cCodigo")]
public string? CCodigo { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
