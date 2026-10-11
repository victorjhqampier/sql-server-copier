using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_transferencia_limites", ExplicitColumns = true)]
internal sealed class SiFinTipoTransferenciaLimites
{
    [PartitionKey(0)]
[Column("idTipoTransferencia")]
public int idTipoTransferencia { get; init; }

    [Column("cTipoTransferencia")]
public string cTipoTransferencia { get; init; }

    [Column("idClasificacionTransferencia")]
public int idClasificacionTransferencia { get; init; }
}
