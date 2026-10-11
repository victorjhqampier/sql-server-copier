using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_det_depos_cts", ExplicitColumns = true)]
internal sealed class SiFinDetDeposCts
{
    [PartitionKey(0)]
[Column("idKardex")]
public int IdKardex { get; init; }

    [Column("idCuenta")]
public int IdCuenta { get; init; }

    [Column("nMontoIntangible")]
public decimal NMontoIntangible { get; init; }

    [Column("nMontoDisp")]
public decimal NMontoDisp { get; init; }

    [Column("idTipoOperacion")]
public int IdTipoOperacion { get; init; }

    [Column("lVigente")]
public bool LVigente { get; init; }
}
