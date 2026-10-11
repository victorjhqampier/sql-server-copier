using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_kardex", ExplicitColumns = true)]
internal sealed class SiFinKardex
{
    [PartitionKey(0)]
[Column("IdKardex")]
public int IdKardex { get; init; }

    [Column("dFechaOpe")]
public Cassandra.LocalDate DFechaOpe { get; init; }

    [Column("dFecHoraOpe")]
public System.DateTime DFecHoraOpe { get; init; }

    [Column("idProducto")]
public int? IdProducto { get; init; }

    [Column("idCanal")]
public int IdCanal { get; init; }

    [Column("idMoneda")]
public int IdMoneda { get; init; }

    [Column("IdUsuario")]
public int? IdUsuario { get; init; }

    [Column("idCuenta")]
public int IdCuenta { get; init; }

    [Column("idTipoOperacion")]
public int IdTipoOperacion { get; init; }

    [Column("idEstadoKardex")]
public int IdEstadoKardex { get; init; }

    [Column("IdKardexExtorno")]
public int? IdKardexExtorno { get; init; }

    [Column("nMontoOperacion")]
public decimal NMontoOperacion { get; init; }

    [Column("nMontoCapital")]
public decimal NMontoCapital { get; init; }

    [Column("nMontoInteres")]
public decimal NMontoInteres { get; init; }

    [Column("nMontoMora")]
public decimal NMontoMora { get; init; }

    [Column("nMontoOtros")]
public decimal NMontoOtros { get; init; }

    [Column("nITFNormal")]
public decimal? NItfNormal { get; init; }

    [Column("idTipoIngresoEgreso")]
public int IdTipoIngresoEgreso { get; init; }

    [Column("IdMotivoExt")]
public int? IdMotivoExt { get; init; }

    [Column("cObservaExt")]
public string? CObservaExt { get; init; }

    [Column("idAgeSaldo")]
public int? IdAgeSaldo { get; init; }

    [Column("idAgeOpera")]
public int? IdAgeOpera { get; init; }

    [Column("idModulo")]
public int? IdModulo { get; init; }

    [Column("idTipoAfeCaj")]
public int? IdTipoAfeCaj { get; init; }

    [Column("nAtrasoPago")]
public short? NAtrasoPago { get; init; }

    [Column("idTipoPago")]
public int? IdTipoPago { get; init; }

    [Column("idTipoPersona")]
public int? IdTipoPersona { get; init; }

    [Column("idCliAfeITF")]
public int? IdCliAfeItf { get; init; }

    [Column("cNroDNI")]
public string? CNroDni { get; init; }

    [Column("cNomCliente")]
public string? CNomCliente { get; init; }

    [Column("cDireccion")]
public string? CDireccion { get; init; }

    [Column("idKardexRelacionado")]
public int? IdKardexRelacionado { get; init; }

    [Column("cObservacion")]
public string? CObservacion { get; init; }

    [Column("nMontoIntComp")]
public decimal? NMontoIntComp { get; init; }

    [Column("idMotivoOperacion")]
public int? IdMotivoOperacion { get; init; }

    [Column("idEstablecimiento")]
public int? IdEstablecimiento { get; init; }

    [Column("idTipoTrxOpe")]
public int? IdTipoTrxOpe { get; init; }

    [Column("dFechaValor")]
public Cassandra.LocalDate? DFechaValor { get; init; }
}
