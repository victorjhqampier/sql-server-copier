using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_clasificacion_transferencia", ExplicitColumns = true)]
internal sealed class SiFinClasificacionTransferencia
{
    [PartitionKey(0)]
[Column("idClasificacionTransferencia")]
public int IdClasificacionTransferencia { get; init; }

    [Column("cTipoEntidad")]
public string CTipoEntidad { get; init; }
}
