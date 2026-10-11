using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_producto", ExplicitColumns = true)]
internal sealed class SiFinProducto
{
    [PartitionKey(0)]
[Column("IdProducto")]
public int IdProducto { get; init; }

    [Column("cCuentaContable")]
public string? CCuentaContable { get; init; }

    [Column("cProducto")]
public string? CProducto { get; init; }

    [Column("IdProductoPadre")]
public int? IdProductoPadre { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }

    [Column("idModulo")]
public int? IdModulo { get; init; }

    [Column("cCodSBS")]
public string? CCodSbs { get; init; }

    [Column("dVigenciaInicio")]
public Cassandra.LocalDate? DVigenciaInicio { get; init; }

    [Column("dVigenciaFin")]
public Cassandra.LocalDate? DVigenciaFin { get; init; }

    [Column("lConfigurable")]
public bool? LConfigurable { get; init; }

    [Column("idFuenteCalcMora")]
public int? IdFuenteCalcMora { get; init; }

    [Column("cProductoCorto")]
public string? CProductoCorto { get; init; }

    [Column("lAhorroWeb")]
public bool? LAhorroWeb { get; init; }

    [Column("idGrupoCamp")]
public int? IdGrupoCamp { get; init; }

    [Column("lTasaClasifInterna")]
public bool? LTasaClasifInterna { get; init; }

    [Column("cTipoOperacionCPE")]
public string? CTipoOperacionCpe { get; init; }
}
