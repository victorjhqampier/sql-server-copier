using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_parametro_prod", ExplicitColumns = true)]
internal sealed class SiFinParametroProd
{
    [PartitionKey(0)]
[Column("IdProducto")]
public int IdProducto { get; init; }

    [PartitionKey(1)]
[Column("IdMoneda")]
public int IdMoneda { get; init; }

    [Column("lIsInactiva")]
public bool LIsInactiva { get; init; }

    [Column("nPlaInactiva")]
public int NPlaInactiva { get; init; }

    [Column("nPlaCancInac")]
public int NPlaCancInac { get; init; }

    [Column("nMonMinApe")]
public decimal NMonMinApe { get; init; }

    [Column("nMonMinSalDis")]
public decimal NMonMinSalDis { get; init; }

    [Column("nMonMinOpe")]
public decimal? NMonMinOpe { get; init; }

    [Column("nPlaMinProd")]
public int NPlaMinProd { get; init; }

    [Column("nPlaMaxProd")]
public int NPlaMaxProd { get; init; }

    [Column("lIsPlazoProd")]
public bool LIsPlazoProd { get; init; }

    [Column("lIsRenovProd")]
public bool LIsRenovProd { get; init; }

    [Column("lIsRequeCert")]
public bool LIsRequeCert { get; init; }

    [Column("lIsAfectoITF")]
public bool LIsAfectoItf { get; init; }

    [Column("lIsTrabFinan")]
public bool? LIsTrabFinan { get; init; }

    [Column("lIsReqPagInt")]
public bool? LIsReqPagInt { get; init; }

    [Column("lIsDefTipPagInt")]
public bool? LIsDefTipPagInt { get; init; }

    [Column("lIsReqEmpleador")]
public bool? LIsReqEmpleador { get; init; }

    [Column("lIsProdTransf")]
public bool? LIsProdTransf { get; init; }

    [Column("lIsProdCTS")]
public bool? LIsProdCts { get; init; }

    [Column("lIsDepAhoProg")]
public bool? LIsDepAhoProg { get; init; }

    [Column("lVigente")]
public bool LVigente { get; init; }

    [Column("nTipCalInt")]
public int? NTipCalInt { get; init; }

    [Column("nPeriodoCap")]
public int? NPeriodoCap { get; init; }

    [Column("lIsOpeRetiro")]
public bool? LIsOpeRetiro { get; init; }

    [Column("lIsOpeDeposito")]
public bool? LIsOpeDeposito { get; init; }

    [Column("lIsCtaCteRel")]
public bool? LIsCtaCteRel { get; init; }

    [Column("idProdTasCancel")]
public int? IdProdTasCancel { get; init; }

    [Column("lIsCtaOrdPago")]
public bool? LIsCtaOrdPago { get; init; }

    [Column("idClasifProdDep")]
public int? IdClasifProdDep { get; init; }

    [Column("lIsDepMenEdad")]
public bool? LIsDepMenEdad { get; init; }

    [Column("idTipoCapDep")]
public int? IdTipoCapDep { get; init; }

    [Column("lIsGarantia")]
public bool? LIsGarantia { get; init; }

    [Column("lActualizaTasaRen")]
public bool? LActualizaTasaRen { get; init; }

    [Column("idProdTasaRenov")]
public int? IdProdTasaRenov { get; init; }

    [Column("nMonMaxApe")]
public decimal? NMonMaxApe { get; init; }

    [Column("nPlaMinProdMeses")]
public int? NPlaMinProdMeses { get; init; }

    [Column("nPlaMaxProdMeses")]
public int? NPlaMaxProdMeses { get; init; }
}
