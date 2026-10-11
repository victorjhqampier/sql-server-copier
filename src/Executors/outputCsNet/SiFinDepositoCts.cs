using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_deposito_cts", ExplicitColumns = true)]
internal sealed class SiFinDepositoCts
{
    [PartitionKey(0)]
[Column("idCuenta")]
public int idCuenta { get; init; }

    [Column("nMontoIntangible")]
public decimal nMontoIntangible { get; init; }

    [Column("nMontoDisp")]
public decimal? nMontoDisp { get; init; }

    [Column("idEntidad")]
public int? idEntidad { get; init; }

    [Column("idEmpleador")]
public int? idEmpleador { get; init; }
}
