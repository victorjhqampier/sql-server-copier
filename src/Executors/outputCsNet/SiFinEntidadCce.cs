using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_entidad_cce", ExplicitColumns = true)]
internal sealed class SiFinEntidadCce
{
    [PartitionKey(0)]
[Column("idEntidadCCE")]
public int idEntidadCCE { get; init; }

    [Column("cNombre")]
public string? cNombre { get; init; }

    [Column("cNombreCorto")]
public string? cNombreCorto { get; init; }

    [Column("cAbreviatura")]
public string? cAbreviatura { get; init; }

    [Column("cCodigoCCE")]
public string? cCodigoCCE { get; init; }

    [Column("lDirectorio")]
public bool? lDirectorio { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
