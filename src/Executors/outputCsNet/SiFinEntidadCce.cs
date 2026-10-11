using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_entidad_cce", ExplicitColumns = true)]
internal sealed class SiFinEntidadCce
{
    [PartitionKey(0)]
[Column("idEntidadCCE")]
public int IdEntidadCce { get; init; }

    [Column("cNombre")]
public string? CNombre { get; init; }

    [Column("cNombreCorto")]
public string? CNombreCorto { get; init; }

    [Column("cAbreviatura")]
public string? CAbreviatura { get; init; }

    [Column("cCodigoCCE")]
public string? CCodigoCce { get; init; }

    [Column("lDirectorio")]
public bool? LDirectorio { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
