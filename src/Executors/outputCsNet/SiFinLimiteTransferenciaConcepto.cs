using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_limite_transferencia_concepto", ExplicitColumns = true)]
internal sealed class SiFinLimiteTransferenciaConcepto
{
    [PartitionKey(0)]
[Column("IdLimiteTransferencia")]
public int IdLimiteTransferencia { get; init; }

    [PartitionKey(1)]
[Column("IdConceptoComision")]
public int IdConceptoComision { get; init; }
}
