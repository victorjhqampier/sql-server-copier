using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_producto", ExplicitColumns = true)]
internal sealed class SiFinProducto
{
    [PartitionKey(0)]
[Column("IdProducto")]
public int IdProducto { get; init; }

    [Column("cCuentaContable")]
public string? cCuentaContable { get; init; }

    [Column("cProducto")]
public string? cProducto { get; init; }

    [Column("IdProductoPadre")]
public int? IdProductoPadre { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }

    [Column("idModulo")]
public int? idModulo { get; init; }

    [Column("cCodSBS")]
public string? cCodSBS { get; init; }

    [Column("dVigenciaInicio")]
public Cassandra.LocalDate? dVigenciaInicio { get; init; }

    [Column("dVigenciaFin")]
public Cassandra.LocalDate? dVigenciaFin { get; init; }

    [Column("lConfigurable")]
public bool? lConfigurable { get; init; }

    [Column("idFuenteCalcMora")]
public int? idFuenteCalcMora { get; init; }

    [Column("cProductoCorto")]
public string? cProductoCorto { get; init; }

    [Column("lAhorroWeb")]
public bool? lAhorroWeb { get; init; }

    [Column("idGrupoCamp")]
public int? idGrupoCamp { get; init; }

    [Column("lTasaClasifInterna")]
public bool? lTasaClasifInterna { get; init; }

    [Column("cTipoOperacionCPE")]
public string? cTipoOperacionCPE { get; init; }
}
