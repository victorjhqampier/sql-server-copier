using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_detalle_ope_rechazado", ExplicitColumns = true)]
internal sealed class SiFinDetalleOpeRechazado
{
    [PartitionKey(0)]
[Column("idDetalleOpeRechazado")]
public int IdDetalleOpeRechazado { get; init; }

    [Column("nMontoOperacion")]
public decimal? NMontoOperacion { get; init; }

    [Column("idTipoOperacion")]
public int? IdTipoOperacion { get; init; }

    [Column("idEntidadOperacion")]
public int? IdEntidadOperacion { get; init; }

    [Column("nComision")]
public decimal? NComision { get; init; }

    [Column("idCargoComision")]
public int? IdCargoComision { get; init; }

    [Column("idAmbitoTransferencia")]
public int? IdAmbitoTransferencia { get; init; }

    [Column("idConceptoComision")]
public int? IdConceptoComision { get; init; }

    [Column("idModalidadConsultaCCI")]
public int? IdModalidadConsultaCci { get; init; }

    [Column("cNumeroCCIOrigen")]
public string? CNumeroCciOrigen { get; init; }

    [Column("cNumeroCCIDestino")]
public string? CNumeroCciDestino { get; init; }

    [Column("cCodigoOperacion")]
public string? CCodigoOperacion { get; init; }

    [Column("cCodigoOperacionOrden")]
public string? CCodigoOperacionOrden { get; init; }

    [Column("cNumeroCelDestino")]
public string? CNumeroCelDestino { get; init; }

    [Column("lRechazadoCCE")]
public bool? LRechazadoCce { get; init; }

    [Column("cCodigoRechazado")]
public string? CCodigoRechazado { get; init; }

    [Column("dFechaHoraOpe")]
public System.DateTime? DFechaHoraOpe { get; init; }

    [Column("idCanal")]
public int? IdCanal { get; init; }

    [Column("cDocumentoID")]
public string? CDocumentoId { get; init; }

    [Column("idMoneda")]
public int? IdMoneda { get; init; }

    [Column("dFechaHoraCCEOrden")]
public System.DateTime? DFechaHoraCceOrden { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
