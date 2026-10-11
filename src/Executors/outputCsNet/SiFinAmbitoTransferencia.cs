using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_ambito_transferencia", ExplicitColumns = true)]
internal sealed class SiFinAmbitoTransferencia
{
    [PartitionKey(0)]
[Column("idAmbitoTransferencia")]
public int IdAmbitoTransferencia { get; init; }

    [Column("cAmbitoTransferencia")]
public string? CAmbitoTransferencia { get; init; }

    [Column("cNombreCorto")]
public string? CNombreCorto { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
