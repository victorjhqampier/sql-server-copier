using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_transaccion", ExplicitColumns = true)]
internal sealed class SiFinTipoTransaccion
{
    [PartitionKey(0)]
[Column("idTipoTransaccion")]
public int IdTipoTransaccion { get; init; }

    [Column("cTipoTransaccion")]
public string? CTipoTransaccion { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
