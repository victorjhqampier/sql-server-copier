using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_deposito_cts", ExplicitColumns = true)]
internal sealed class SiFinDepositoCts
{
    [PartitionKey(0)]
[Column("idCuenta")]
public int IdCuenta { get; init; }

    [Column("nMontoIntangible")]
public decimal NMontoIntangible { get; init; }

    [Column("nMontoDisp")]
public decimal? NMontoDisp { get; init; }

    [Column("idEntidad")]
public int? IdEntidad { get; init; }

    [Column("idEmpleador")]
public int? IdEmpleador { get; init; }
}
