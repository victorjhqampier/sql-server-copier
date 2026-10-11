using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_detalle_transf_kardex", ExplicitColumns = true)]
internal sealed class SiFinDetalleTransfKardex
{
    [PartitionKey(0)]
[Column("idDetalleTransfKardex")]
public int idDetalleTransfKardex { get; init; }

    [Column("idKardex")]
public int? idKardex { get; init; }

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

    [Column("cNumeroCCI")]
public string? cNumeroCCI { get; init; }

    [Column("cCodigoOperacion")]
public string? cCodigoOperacion { get; init; }

    [Column("cCodigoOperacionOrden")]
public string? cCodigoOperacionOrden { get; init; }

    [Column("cCodigoInstruccion")]
public string? cCodigoInstruccion { get; init; }

    [Column("cNumeroCelDestino")]
public string? cNumeroCelDestino { get; init; }

    [Column("dFechaHoraOpe")]
public System.DateTime? dFechaHoraOpe { get; init; }

    [Column("dFechaHoraCCEOrden")]
public System.DateTime? dFechaHoraCCEOrden { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
