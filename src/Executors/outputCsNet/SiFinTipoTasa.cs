using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_tasa", ExplicitColumns = true)]
internal sealed class SiFinTipoTasa
{
    [PartitionKey(0)]
[Column("idTipoTasa")]
public int idTipoTasa { get; init; }

    [Column("cTipoTasa")]
public string? cTipoTasa { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }

    [Column("idModulo")]
public int? idModulo { get; init; }
}
