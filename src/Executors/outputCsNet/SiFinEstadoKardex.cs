using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_estado_kardex", ExplicitColumns = true)]
internal sealed class SiFinEstadoKardex
{
    [PartitionKey(0)]
[Column("idEstadoKardex")]
public int IdEstadoKardex { get; init; }

    [Column("cEstadoKardex")]
public string? CEstadoKardex { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }

    [Column("lTipoEstadoOpe")]
public bool? LTipoEstadoOpe { get; init; }
}
