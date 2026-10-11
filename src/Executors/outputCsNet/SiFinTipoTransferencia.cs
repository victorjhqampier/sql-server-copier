using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_transferencia", ExplicitColumns = true)]
internal sealed class SiFinTipoTransferencia
{
    [PartitionKey(0)]
[Column("idTiTransfer")]
public int idTiTransfer { get; init; }

    [Column("cDescripcion")]
public string? cDescripcion { get; init; }

    [Column("lEstado")]
public bool lEstado { get; init; }

    [Column("idTipoOpeVinc")]
public int? idTipoOpeVinc { get; init; }
}
