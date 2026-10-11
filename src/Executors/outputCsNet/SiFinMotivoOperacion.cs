using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_motivo_operacion", ExplicitColumns = true)]
internal sealed class SiFinMotivoOperacion
{
    [PartitionKey(0)]
[Column("idMotivoOperacion")]
public int IdMotivoOperacion { get; init; }

    [Column("idTipoOperacion")]
public int IdTipoOperacion { get; init; }

    [Column("cMotivoOperacion")]
public string CMotivoOperacion { get; init; }

    [Column("lVigente")]
public bool LVigente { get; init; }

    [Column("lReqAutorizacion")]
public bool? LReqAutorizacion { get; init; }
}
