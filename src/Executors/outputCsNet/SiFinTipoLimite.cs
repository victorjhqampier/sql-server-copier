using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_limite", ExplicitColumns = true)]
internal sealed class SiFinTipoLimite
{
    [PartitionKey(0)]
[Column("idTipoLimite")]
public int idTipoLimite { get; init; }

    [Column("cNombre")]
public string cNombre { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
