using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_motivo_operacion", ExplicitColumns = true)]
internal sealed class SiFinMotivoOperacion
{
    [PartitionKey(0)]
[Column("idMotivoOperacion")]
public int idMotivoOperacion { get; init; }

    [Column("idTipoOperacion")]
public int idTipoOperacion { get; init; }

    [Column("cMotivoOperacion")]
public string cMotivoOperacion { get; init; }

    [Column("lVigente")]
public bool lVigente { get; init; }

    [Column("lReqAutorizacion")]
public bool? lReqAutorizacion { get; init; }
}
