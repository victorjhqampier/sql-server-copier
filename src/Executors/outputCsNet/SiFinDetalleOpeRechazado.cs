using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_detalle_ope_rechazado", ExplicitColumns = true)]
internal sealed class SiFinDetalleOpeRechazado
{
    [PartitionKey(0)]
[Column("idDetalleOpeRechazado")]
public int idDetalleOpeRechazado { get; init; }

    [Column("nMontoOperacion")]
public decimal? nMontoOperacion { get; init; }

    [Column("idTipoOperacion")]
public int? idTipoOperacion { get; init; }

    [Column("idEntidadOperacion")]
public int? idEntidadOperacion { get; init; }

    [Column("nComision")]
public decimal? nComision { get; init; }

    [Column("idCargoComision")]
public int? idCargoComision { get; init; }

    [Column("idAmbitoTransferencia")]
public int? idAmbitoTransferencia { get; init; }

    [Column("idConceptoComision")]
public int? idConceptoComision { get; init; }

    [Column("idModalidadConsultaCCI")]
public int? idModalidadConsultaCCI { get; init; }

    [Column("cNumeroCCIOrigen")]
public string? cNumeroCCIOrigen { get; init; }

    [Column("cNumeroCCIDestino")]
public string? cNumeroCCIDestino { get; init; }

    [Column("cCodigoOperacion")]
public string? cCodigoOperacion { get; init; }

    [Column("cCodigoOperacionOrden")]
public string? cCodigoOperacionOrden { get; init; }

    [Column("cNumeroCelDestino")]
public string? cNumeroCelDestino { get; init; }

    [Column("lRechazadoCCE")]
public bool? lRechazadoCCE { get; init; }

    [Column("cCodigoRechazado")]
public string? cCodigoRechazado { get; init; }

    [Column("dFechaHoraOpe")]
public System.DateTime? dFechaHoraOpe { get; init; }

    [Column("idCanal")]
public int? idCanal { get; init; }

    [Column("cDocumentoID")]
public string? cDocumentoID { get; init; }

    [Column("idMoneda")]
public int? idMoneda { get; init; }

    [Column("dFechaHoraCCEOrden")]
public System.DateTime? dFechaHoraCCEOrden { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
