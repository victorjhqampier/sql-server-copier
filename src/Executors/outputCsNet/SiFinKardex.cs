using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_kardex", ExplicitColumns = true)]
internal sealed class SiFinKardex
{
    [PartitionKey(0)]
[Column("IdKardex")]
public int IdKardex { get; init; }

    [Column("dFechaOpe")]
public Cassandra.LocalDate dFechaOpe { get; init; }

    [Column("dFecHoraOpe")]
public System.DateTime dFecHoraOpe { get; init; }

    [Column("idProducto")]
public int? idProducto { get; init; }

    [Column("idCanal")]
public int idCanal { get; init; }

    [Column("idMoneda")]
public int idMoneda { get; init; }

    [Column("IdUsuario")]
public int? IdUsuario { get; init; }

    [Column("idCuenta")]
public int idCuenta { get; init; }

    [Column("idTipoOperacion")]
public int idTipoOperacion { get; init; }

    [Column("idEstadoKardex")]
public int idEstadoKardex { get; init; }

    [Column("IdKardexExtorno")]
public int? IdKardexExtorno { get; init; }

    [Column("nMontoOperacion")]
public decimal nMontoOperacion { get; init; }

    [Column("nMontoCapital")]
public decimal nMontoCapital { get; init; }

    [Column("nMontoInteres")]
public decimal nMontoInteres { get; init; }

    [Column("nMontoMora")]
public decimal nMontoMora { get; init; }

    [Column("nMontoOtros")]
public decimal nMontoOtros { get; init; }

    [Column("nITFNormal")]
public decimal? nITFNormal { get; init; }

    [Column("idTipoIngresoEgreso")]
public int idTipoIngresoEgreso { get; init; }

    [Column("IdMotivoExt")]
public int? IdMotivoExt { get; init; }

    [Column("cObservaExt")]
public string? cObservaExt { get; init; }

    [Column("idAgeSaldo")]
public int? idAgeSaldo { get; init; }

    [Column("idAgeOpera")]
public int? idAgeOpera { get; init; }

    [Column("idModulo")]
public int? idModulo { get; init; }

    [Column("idTipoAfeCaj")]
public int? idTipoAfeCaj { get; init; }

    [Column("nAtrasoPago")]
public short? nAtrasoPago { get; init; }

    [Column("idTipoPago")]
public int? idTipoPago { get; init; }

    [Column("idTipoPersona")]
public int? idTipoPersona { get; init; }

    [Column("idCliAfeITF")]
public int? idCliAfeITF { get; init; }

    [Column("cNroDNI")]
public string? cNroDNI { get; init; }

    [Column("cNomCliente")]
public string? cNomCliente { get; init; }

    [Column("cDireccion")]
public string? cDireccion { get; init; }

    [Column("idKardexRelacionado")]
public int? idKardexRelacionado { get; init; }

    [Column("cObservacion")]
public string? cObservacion { get; init; }

    [Column("nMontoIntComp")]
public decimal? nMontoIntComp { get; init; }

    [Column("idMotivoOperacion")]
public int? idMotivoOperacion { get; init; }

    [Column("idEstablecimiento")]
public int? idEstablecimiento { get; init; }

    [Column("idTipoTrxOpe")]
public int? idTipoTrxOpe { get; init; }

    [Column("dFechaValor")]
public Cassandra.LocalDate? dFechaValor { get; init; }
}
