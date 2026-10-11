using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_transferencia", ExplicitColumns = true)]
internal sealed class SiFinTipoTransferencia
{
    [PartitionKey(0)]
[Column("idTiTransfer")]
public int IdTiTransfer { get; init; }

    [Column("cDescripcion")]
public string? CDescripcion { get; init; }

    [Column("lEstado")]
public bool LEstado { get; init; }

    [Column("idTipoOpeVinc")]
public int? IdTipoOpeVinc { get; init; }
}
