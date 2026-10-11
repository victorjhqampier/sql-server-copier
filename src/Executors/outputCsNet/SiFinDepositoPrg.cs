using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_deposito_prg", ExplicitColumns = true)]
internal sealed class SiFinDepositoPrg
{
    [PartitionKey(0)]
[Column("idCuenta")]
public int IdCuenta { get; init; }

    [Column("nNroDepos")]
public int NNroDepos { get; init; }

    [Column("nNumDepPag")]
public int NNumDepPag { get; init; }

    [Column("nDiasAtrazo")]
public int NDiasAtrazo { get; init; }

    [Column("nMontoCuota")]
public decimal? NMontoCuota { get; init; }

    [Column("nMonAdelantado")]
public decimal? NMonAdelantado { get; init; }

    [Column("nMonTotProg")]
public decimal? NMonTotProg { get; init; }

    [Column("idPlanPagos")]
public int? IdPlanPagos { get; init; }

    [Column("dFechaCulminacion")]
public Cassandra.LocalDate? DFechaCulminacion { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }

    [Column("idMotivo")]
public int? IdMotivo { get; init; }

    [Column("lDebito")]
public bool LDebito { get; init; }

    [Column("nMesGracia")]
public int NMesGracia { get; init; }

    [Column("lBeneficio")]
public bool LBeneficio { get; init; }

    [Column("lCmpContrato")]
public bool LCmpContrato { get; init; }

    [Column("idCtaDebitoPrg")]
public int IdCtaDebitoPrg { get; init; }

    [Column("idBonificacion")]
public int IdBonificacion { get; init; }

    [Column("idEstadoCta")]
public int IdEstadoCta { get; init; }
}
