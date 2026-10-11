using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_deposito_prg", ExplicitColumns = true)]
internal sealed class SiFinDepositoPrg
{
    [PartitionKey(0)]
[Column("idCuenta")]
public int idCuenta { get; init; }

    [Column("nNroDepos")]
public int nNroDepos { get; init; }

    [Column("nNumDepPag")]
public int nNumDepPag { get; init; }

    [Column("nDiasAtrazo")]
public int nDiasAtrazo { get; init; }

    [Column("nMontoCuota")]
public decimal? nMontoCuota { get; init; }

    [Column("nMonAdelantado")]
public decimal? nMonAdelantado { get; init; }

    [Column("nMonTotProg")]
public decimal? nMonTotProg { get; init; }

    [Column("idPlanPagos")]
public int? idPlanPagos { get; init; }

    [Column("dFechaCulminacion")]
public Cassandra.LocalDate? dFechaCulminacion { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }

    [Column("idMotivo")]
public int? idMotivo { get; init; }

    [Column("lDebito")]
public bool lDebito { get; init; }

    [Column("nMesGracia")]
public int nMesGracia { get; init; }

    [Column("lBeneficio")]
public bool lBeneficio { get; init; }

    [Column("lCmpContrato")]
public bool lCmpContrato { get; init; }

    [Column("idCtaDebitoPrg")]
public int idCtaDebitoPrg { get; init; }

    [Column("idBonificacion")]
public int idBonificacion { get; init; }

    [Column("idEstadoCta")]
public int idEstadoCta { get; init; }
}
