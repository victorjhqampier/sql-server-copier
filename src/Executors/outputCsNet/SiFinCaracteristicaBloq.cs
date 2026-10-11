using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_caracteristica_bloq", ExplicitColumns = true)]
internal sealed class SiFinCaracteristicaBloq
{
    [PartitionKey(0)]
[Column("idCaracteristicaBloq")]
public int IdCaracteristicaBloq { get; init; }

    [Column("cDescripcion")]
public string CDescripcion { get; init; }

    [Column("lIsReqMonto")]
public bool LIsReqMonto { get; init; }

    [Column("lEstado")]
public bool LEstado { get; init; }

    [Column("idTipoOperacion")]
public int? IdTipoOperacion { get; init; }
}
