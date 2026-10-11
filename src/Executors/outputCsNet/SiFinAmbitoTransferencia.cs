using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_ambito_transferencia", ExplicitColumns = true)]
internal sealed class SiFinAmbitoTransferencia
{
    [PartitionKey(0)]
[Column("idAmbitoTransferencia")]
public int idAmbitoTransferencia { get; init; }

    [Column("cAmbitoTransferencia")]
public string? cAmbitoTransferencia { get; init; }

    [Column("cNombreCorto")]
public string? cNombreCorto { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
