using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_estado_kardex", ExplicitColumns = true)]
internal sealed class SiFinEstadoKardex
{
    [PartitionKey(0)]
[Column("idEstadoKardex")]
public int idEstadoKardex { get; init; }

    [Column("cEstadoKardex")]
public string? cEstadoKardex { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }

    [Column("lTipoEstadoOpe")]
public bool? lTipoEstadoOpe { get; init; }
}
