using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_outbox_transaccion", ExplicitColumns = true)]
internal sealed class SiFinOutboxTransaccion
{
    [PartitionKey(0)]
[Column("idOutbox")]
public long IdOutbox { get; init; }

    [Column("idTrxIdempotency")]
public System.Guid IdTrxIdempotency { get; init; }

    [Column("idCuenta")]
public int IdCuenta { get; init; }

    [Column("nMonto")]
public decimal NMonto { get; init; }

    [Column("lEsRetiro")]
public bool LEsRetiro { get; init; }

    [Column("lEstaCerrado")]
public bool LEstaCerrado { get; init; }

    [Column("dFechaRegistro")]
public System.DateTime DFechaRegistro { get; init; }

    [Column("dfechaCierre")]
public System.DateTime? DfechaCierre { get; init; }
}
