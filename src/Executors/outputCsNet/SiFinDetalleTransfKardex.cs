using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_detalle_transf_kardex", ExplicitColumns = true)]
internal sealed class SiFinDetalleTransfKardex
{
    [PartitionKey(0)]
[Column("idDetalleTransfKardex")]
public int IdDetalleTransfKardex { get; init; }

    [Column("idKardex")]
public int? IdKardex { get; init; }

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

    [Column("cNumeroCCI")]
public string? CNumeroCci { get; init; }

    [Column("cCodigoOperacion")]
public string? CCodigoOperacion { get; init; }

    [Column("cCodigoOperacionOrden")]
public string? CCodigoOperacionOrden { get; init; }

    [Column("cCodigoInstruccion")]
public string? CCodigoInstruccion { get; init; }

    [Column("cNumeroCelDestino")]
public string? CNumeroCelDestino { get; init; }

    [Column("dFechaHoraOpe")]
public System.DateTime? DFechaHoraOpe { get; init; }

    [Column("dFechaHoraCCEOrden")]
public System.DateTime? DFechaHoraCceOrden { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
