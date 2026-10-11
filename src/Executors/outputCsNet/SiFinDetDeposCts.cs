using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_det_depos_cts", ExplicitColumns = true)]
internal sealed class SiFinDetDeposCts
{
    [PartitionKey(0)]
[Column("idKardex")]
public int idKardex { get; init; }

    [Column("idCuenta")]
public int idCuenta { get; init; }

    [Column("nMontoIntangible")]
public decimal nMontoIntangible { get; init; }

    [Column("nMontoDisp")]
public decimal nMontoDisp { get; init; }

    [Column("idTipoOperacion")]
public int idTipoOperacion { get; init; }

    [Column("lVigente")]
public bool lVigente { get; init; }
}
