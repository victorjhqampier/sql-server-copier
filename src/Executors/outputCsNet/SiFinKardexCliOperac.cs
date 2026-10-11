using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_kardex_cli_operac", ExplicitColumns = true)]
internal sealed class SiFinKardexCliOperac
{
    [PartitionKey(0)]
[Column("idKardex")]
public int IdKardex { get; init; }

    [PartitionKey(1)]
[Column("idCli")]
public int IdCli { get; init; }

    [PartitionKey(2)]
[Column("lVigente")]
public bool LVigente { get; init; }
}
