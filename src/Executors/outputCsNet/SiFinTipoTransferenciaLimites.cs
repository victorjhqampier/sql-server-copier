using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_transferencia_limites", ExplicitColumns = true)]
internal sealed class SiFinTipoTransferenciaLimites
{
    [PartitionKey(0)]
[Column("idTipoTransferencia")]
public int IdTipoTransferencia { get; init; }

    [Column("cTipoTransferencia")]
public string CTipoTransferencia { get; init; }

    [Column("idClasificacionTransferencia")]
public int IdClasificacionTransferencia { get; init; }
}
