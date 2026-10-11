using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_caracteristica_bloq", ExplicitColumns = true)]
internal sealed class SiFinCaracteristicaBloq
{
    [PartitionKey(0)]
[Column("idCaracteristicaBloq")]
public int idCaracteristicaBloq { get; init; }

    [Column("cDescripcion")]
public string cDescripcion { get; init; }

    [Column("lIsReqMonto")]
public bool lIsReqMonto { get; init; }

    [Column("lEstado")]
public bool lEstado { get; init; }

    [Column("idTipoOperacion")]
public int? idTipoOperacion { get; init; }
}
