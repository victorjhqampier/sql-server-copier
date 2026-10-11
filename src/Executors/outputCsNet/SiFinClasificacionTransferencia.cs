using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_clasificacion_transferencia", ExplicitColumns = true)]
internal sealed class SiFinClasificacionTransferencia
{
    [PartitionKey(0)]
[Column("idClasificacionTransferencia")]
public int idClasificacionTransferencia { get; init; }

    [Column("cTipoEntidad")]
public string cTipoEntidad { get; init; }
}
