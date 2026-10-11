using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_directorio_cce", ExplicitColumns = true)]
internal sealed class SiFinDirectorioCce
{
    [PartitionKey(0)]
[Column("idDirectorioCCE")]
public int IdDirectorioCce { get; init; }

    [Column("cNombre")]
public string CNombre { get; init; }

    [Column("cCodigo")]
public string CCodigo { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
