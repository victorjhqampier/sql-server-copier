using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_remu_cliente_cts", ExplicitColumns = true)]
internal sealed class SiFinRemuClienteCts
{
    [PartitionKey(0)]
[Column("idCuenta")]
public int idCuenta { get; init; }

    [PartitionKey(1)]
[Column("idPeriodo")]
public int idPeriodo { get; init; }

    [Column("idMoneda")]
public int idMoneda { get; init; }

    [Column("nRemuTotal")]
public decimal nRemuTotal { get; init; }
}
