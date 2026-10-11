using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_cuenta", ExplicitColumns = true)]
internal sealed class SiFinTipoCuenta
{
    [PartitionKey(0)]
[Column("idTipoCuenta")]
public int IdTipoCuenta { get; init; }

    [Column("cTipoCuenta")]
public string? CTipoCuenta { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
