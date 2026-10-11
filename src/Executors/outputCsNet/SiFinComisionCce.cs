using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_comision_cce", ExplicitColumns = true)]
internal sealed class SiFinComisionCce
{
    [PartitionKey(0)]
[Column("idComisionCCE")]
public int IdComisionCce { get; init; }

    [Column("cCodigo")]
public string? CCodigo { get; init; }

    [Column("cCanalAndes")]
public string? CCanalAndes { get; init; }

    [Column("idCanal")]
public int? IdCanal { get; init; }

    [Column("cTipo")]
public string? CTipo { get; init; }

    [Column("cOrigen")]
public string? COrigen { get; init; }

    [Column("cDestino")]
public string? CDestino { get; init; }

    [Column("dPeriodoInicial")]
public System.DateTime? DPeriodoInicial { get; init; }

    [Column("dPeriodoFinal")]
public System.DateTime? DPeriodoFinal { get; init; }

    [Column("cTipOpeOriginante")]
public string? CTipOpeOriginante { get; init; }

    [Column("cTipoTransferencia")]
public string? CTipoTransferencia { get; init; }

    [Column("cModalidad")]
public string? CModalidad { get; init; }

    [Column("cTipoPersona")]
public string? CTipoPersona { get; init; }

    [Column("cCanal")]
public string? CCanal { get; init; }

    [Column("cCodConcepDebito")]
public string? CCodConcepDebito { get; init; }

    [Column("cMoneda")]
public string? CMoneda { get; init; }

    [Column("idMoneda")]
public int? IdMoneda { get; init; }

    [Column("nMontoMin")]
public decimal? NMontoMin { get; init; }

    [Column("nMontoMax")]
public decimal? NMontoMax { get; init; }

    [Column("nComisionVariable")]
public decimal? NComisionVariable { get; init; }

    [Column("nComisionVarMin")]
public decimal? NComisionVarMin { get; init; }

    [Column("nComisionVarMax")]
public decimal? NComisionVarMax { get; init; }

    [Column("nComisionFija")]
public decimal? NComisionFija { get; init; }

    [Column("cPagadoPor")]
public string? CPagadoPor { get; init; }

    [Column("idUsuReg")]
public int? IdUsuReg { get; init; }

    [Column("dFechaRegistro")]
public System.DateTime? DFechaRegistro { get; init; }

    [Column("dFechaModificacion")]
public System.DateTime? DFechaModificacion { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
