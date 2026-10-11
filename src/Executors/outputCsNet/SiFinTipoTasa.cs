using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_tasa", ExplicitColumns = true)]
internal sealed class SiFinTipoTasa
{
    [PartitionKey(0)]
[Column("idTipoTasa")]
public int IdTipoTasa { get; init; }

    [Column("cTipoTasa")]
public string? CTipoTasa { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }

    [Column("idModulo")]
public int? IdModulo { get; init; }
}
