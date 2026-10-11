using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_pla_pag_dep_pro", ExplicitColumns = true)]
internal sealed class SiFinPlaPagDepPro
{
    [PartitionKey(0)]
[Column("idCuenta")]
public int IdCuenta { get; init; }

    [PartitionKey(1)]
[Column("idPlanPagos")]
public int IdPlanPagos { get; init; }

    [PartitionKey(2)]
[Column("idCuota")]
public int IdCuota { get; init; }

    [PartitionKey(3)]
[Column("idTipoCuota")]
public int IdTipoCuota { get; init; }

    [Column("idEstadoCuota")]
public int? IdEstadoCuota { get; init; }

    [Column("dFechaProg")]
public Cassandra.LocalDate? DFechaProg { get; init; }

    [Column("dFechaPago")]
public Cassandra.LocalDate? DFechaPago { get; init; }

    [Column("nCapital")]
public decimal? NCapital { get; init; }

    [Column("nCapitalPagado")]
public decimal? NCapitalPagado { get; init; }

    [Column("nAtrasoCuota")]
public int? NAtrasoCuota { get; init; }

    [Column("nNumDiaCuota")]
public short? NNumDiaCuota { get; init; }

    [Column("dFechaDesde")]
public Cassandra.LocalDate? DFechaDesde { get; init; }

    [Column("dFechaHasta")]
public Cassandra.LocalDate? DFechaHasta { get; init; }

    [Column("nCapitalAcumuladoEsperado")]
public decimal? NCapitalAcumuladoEsperado { get; init; }

    [Column("nDias")]
public int? NDias { get; init; }

    [Column("dFechaRetiro")]
public Cassandra.LocalDate? DFechaRetiro { get; init; }

    [Column("cKrdRetiroForzoso")]
public string? CKrdRetiroForzoso { get; init; }

    [Column("nSaldoCierre")]
public decimal? NSaldoCierre { get; init; }

    [Column("nCapitalRetirado")]
public decimal? NCapitalRetirado { get; init; }

    [Column("nSaldoCapital")]
public decimal? NSaldoCapital { get; init; }

    [Column("nSaldoDisponible")]
public decimal? NSaldoDisponible { get; init; }

    [Column("nSaldoAnterior")]
public decimal? NSaldoAnterior { get; init; }

    [Column("nIntEsperado")]
public decimal? NIntEsperado { get; init; }
}
