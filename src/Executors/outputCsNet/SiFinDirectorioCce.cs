using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_directorio_cce", ExplicitColumns = true)]
internal sealed class SiFinDirectorioCce
{
    [PartitionKey(0)]
[Column("idDirectorioCCE")]
public int idDirectorioCCE { get; init; }

    [Column("cNombre")]
public string cNombre { get; init; }

    [Column("cCodigo")]
public string cCodigo { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
