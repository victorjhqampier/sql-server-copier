using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_comision_cce", ExplicitColumns = true)]
internal sealed class SiFinComisionCce
{
    [PartitionKey(0)]
[Column("idComisionCCE")]
public int idComisionCCE { get; init; }

    [Column("cCodigo")]
public string? cCodigo { get; init; }

    [Column("cCanalAndes")]
public string? cCanalAndes { get; init; }

    [Column("idCanal")]
public int? idCanal { get; init; }

    [Column("cTipo")]
public string? cTipo { get; init; }

    [Column("cOrigen")]
public string? cOrigen { get; init; }

    [Column("cDestino")]
public string? cDestino { get; init; }

    [Column("dPeriodoInicial")]
public System.DateTime? dPeriodoInicial { get; init; }

    [Column("dPeriodoFinal")]
public System.DateTime? dPeriodoFinal { get; init; }

    [Column("cTipOpeOriginante")]
public string? cTipOpeOriginante { get; init; }

    [Column("cTipoTransferencia")]
public string? cTipoTransferencia { get; init; }

    [Column("cModalidad")]
public string? cModalidad { get; init; }

    [Column("cTipoPersona")]
public string? cTipoPersona { get; init; }

    [Column("cCanal")]
public string? cCanal { get; init; }

    [Column("cCodConcepDebito")]
public string? cCodConcepDebito { get; init; }

    [Column("cMoneda")]
public string? cMoneda { get; init; }

    [Column("idMoneda")]
public int? idMoneda { get; init; }

    [Column("nMontoMin")]
public decimal? nMontoMin { get; init; }

    [Column("nMontoMax")]
public decimal? nMontoMax { get; init; }

    [Column("nComisionVariable")]
public decimal? nComisionVariable { get; init; }

    [Column("nComisionVarMin")]
public decimal? nComisionVarMin { get; init; }

    [Column("nComisionVarMax")]
public decimal? nComisionVarMax { get; init; }

    [Column("nComisionFija")]
public decimal? nComisionFija { get; init; }

    [Column("cPagadoPor")]
public string? cPagadoPor { get; init; }

    [Column("idUsuReg")]
public int? idUsuReg { get; init; }

    [Column("dFechaRegistro")]
public System.DateTime? dFechaRegistro { get; init; }

    [Column("dFechaModificacion")]
public System.DateTime? dFechaModificacion { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
