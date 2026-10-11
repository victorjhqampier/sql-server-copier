using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_outbox_transaccion", ExplicitColumns = true)]
internal sealed class SiFinOutboxTransaccion
{
    [PartitionKey(0)]
[Column("idOutbox")]
public long idOutbox { get; init; }

    [Column("idTrxIdempotency")]
public System.Guid idTrxIdempotency { get; init; }

    [Column("idCuenta")]
public int idCuenta { get; init; }

    [Column("nMonto")]
public decimal nMonto { get; init; }

    [Column("lEsRetiro")]
public bool lEsRetiro { get; init; }

    [Column("lEstaCerrado")]
public bool lEstaCerrado { get; init; }

    [Column("dFechaRegistro")]
public System.DateTime dFechaRegistro { get; init; }

    [Column("dfechaCierre")]
public System.DateTime? dfechaCierre { get; init; }
}
