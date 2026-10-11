using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_transaccion", ExplicitColumns = true)]
internal sealed class SiFinTipoTransaccion
{
    [PartitionKey(0)]
[Column("idTipoTransaccion")]
public int idTipoTransaccion { get; init; }

    [Column("cTipoTransaccion")]
public string? cTipoTransaccion { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
