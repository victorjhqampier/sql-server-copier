using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_operacion", ExplicitColumns = true)]
internal sealed class SiFinTipoOperacion
{
    [PartitionKey(0)]
[Column("idTipoOperacion")]
public int IdTipoOperacion { get; init; }

    [Column("cTipoOperacion")]
public string? CTipoOperacion { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }

    [Column("idModulo")]
public int? IdModulo { get; init; }

    [Column("lIndClaModalidad")]
public bool? LIndClaModalidad { get; init; }

    [Column("lIndClaMoneda")]
public bool? LIndClaMoneda { get; init; }

    [Column("lIndClaProducto")]
public bool? LIndClaProducto { get; init; }

    [Column("lIndExtractoCta")]
public bool? LIndExtractoCta { get; init; }

    [Column("cCodSBS")]
public string? CCodSbs { get; init; }

    [Column("idTipoAsiento")]
public int? IdTipoAsiento { get; init; }

    [Column("nNumIpresionVou")]
public int? NNumIpresionVou { get; init; }

    [Column("lRegOpeSplaft")]
public bool? LRegOpeSplaft { get; init; }

    [Column("cDesOpeSplaft")]
public string? CDesOpeSplaft { get; init; }

    [Column("lImprimir")]
public bool? LImprimir { get; init; }

    [Column("nNumDiasVcto")]
public int NNumDiasVcto { get; init; }
}
