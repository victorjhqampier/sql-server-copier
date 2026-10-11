using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_remu_cliente_cts", ExplicitColumns = true)]
internal sealed class SiFinRemuClienteCts
{
    [PartitionKey(0)]
[Column("idCuenta")]
public int IdCuenta { get; init; }

    [PartitionKey(1)]
[Column("idPeriodo")]
public int IdPeriodo { get; init; }

    [Column("idMoneda")]
public int IdMoneda { get; init; }

    [Column("nRemuTotal")]
public decimal NRemuTotal { get; init; }
}
