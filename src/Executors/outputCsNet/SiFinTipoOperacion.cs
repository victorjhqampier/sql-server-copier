using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_operacion", ExplicitColumns = true)]
internal sealed class SiFinTipoOperacion
{
    [PartitionKey(0)]
[Column("idTipoOperacion")]
public int idTipoOperacion { get; init; }

    [Column("cTipoOperacion")]
public string? cTipoOperacion { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }

    [Column("idModulo")]
public int? idModulo { get; init; }

    [Column("lIndClaModalidad")]
public bool? lIndClaModalidad { get; init; }

    [Column("lIndClaMoneda")]
public bool? lIndClaMoneda { get; init; }

    [Column("lIndClaProducto")]
public bool? lIndClaProducto { get; init; }

    [Column("lIndExtractoCta")]
public bool? lIndExtractoCta { get; init; }

    [Column("cCodSBS")]
public string? cCodSBS { get; init; }

    [Column("idTipoAsiento")]
public int? idTipoAsiento { get; init; }

    [Column("nNumIpresionVou")]
public int? nNumIpresionVou { get; init; }

    [Column("lRegOpeSplaft")]
public bool? lRegOpeSplaft { get; init; }

    [Column("cDesOpeSplaft")]
public string? cDesOpeSplaft { get; init; }

    [Column("lImprimir")]
public bool? lImprimir { get; init; }

    [Column("nNumDiasVcto")]
public int nNumDiasVcto { get; init; }
}
