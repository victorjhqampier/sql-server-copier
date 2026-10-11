using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_limite", ExplicitColumns = true)]
internal sealed class SiFinTipoLimite
{
    [PartitionKey(0)]
[Column("idTipoLimite")]
public int IdTipoLimite { get; init; }

    [Column("cNombre")]
public string CNombre { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
