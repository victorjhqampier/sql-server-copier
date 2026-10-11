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
public bool lIsInactiva { get; init; }

    [Column("nPlaInactiva")]
public int nPlaInactiva { get; init; }

    [Column("nPlaCancInac")]
public int nPlaCancInac { get; init; }

    [Column("nMonMinApe")]
public decimal nMonMinApe { get; init; }

    [Column("nMonMinSalDis")]
public decimal nMonMinSalDis { get; init; }

    [Column("nMonMinOpe")]
public decimal? nMonMinOpe { get; init; }

    [Column("nPlaMinProd")]
public int nPlaMinProd { get; init; }

    [Column("nPlaMaxProd")]
public int nPlaMaxProd { get; init; }

    [Column("lIsPlazoProd")]
public bool lIsPlazoProd { get; init; }

    [Column("lIsRenovProd")]
public bool lIsRenovProd { get; init; }

    [Column("lIsRequeCert")]
public bool lIsRequeCert { get; init; }

    [Column("lIsAfectoITF")]
public bool lIsAfectoITF { get; init; }

    [Column("lIsTrabFinan")]
public bool? lIsTrabFinan { get; init; }

    [Column("lIsReqPagInt")]
public bool? lIsReqPagInt { get; init; }

    [Column("lIsDefTipPagInt")]
public bool? lIsDefTipPagInt { get; init; }

    [Column("lIsReqEmpleador")]
public bool? lIsReqEmpleador { get; init; }

    [Column("lIsProdTransf")]
public bool? lIsProdTransf { get; init; }

    [Column("lIsProdCTS")]
public bool? lIsProdCTS { get; init; }

    [Column("lIsDepAhoProg")]
public bool? lIsDepAhoProg { get; init; }

    [Column("lVigente")]
public bool lVigente { get; init; }

    [Column("nTipCalInt")]
public int? nTipCalInt { get; init; }

    [Column("nPeriodoCap")]
public int? nPeriodoCap { get; init; }

    [Column("lIsOpeRetiro")]
public bool? lIsOpeRetiro { get; init; }

    [Column("lIsOpeDeposito")]
public bool? lIsOpeDeposito { get; init; }

    [Column("lIsCtaCteRel")]
public bool? lIsCtaCteRel { get; init; }

    [Column("idProdTasCancel")]
public int? idProdTasCancel { get; init; }

    [Column("lIsCtaOrdPago")]
public bool? lIsCtaOrdPago { get; init; }

    [Column("idClasifProdDep")]
public int? idClasifProdDep { get; init; }

    [Column("lIsDepMenEdad")]
public bool? lIsDepMenEdad { get; init; }

    [Column("idTipoCapDep")]
public int? idTipoCapDep { get; init; }

    [Column("lIsGarantia")]
public bool? lIsGarantia { get; init; }

    [Column("lActualizaTasaRen")]
public bool? lActualizaTasaRen { get; init; }

    [Column("idProdTasaRenov")]
public int? idProdTasaRenov { get; init; }

    [Column("nMonMaxApe")]
public decimal? nMonMaxApe { get; init; }

    [Column("nPlaMinProdMeses")]
public int? nPlaMinProdMeses { get; init; }

    [Column("nPlaMaxProdMeses")]
public int? nPlaMaxProdMeses { get; init; }
}
