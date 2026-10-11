using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_reg_documento_cts", ExplicitColumns = true)]
internal sealed class SiFinRegDocumentoCts
{
    [PartitionKey(0)]
[Column("idRegDoc")]
public int idRegDoc { get; init; }

    [Column("idCuenta")]
public int idCuenta { get; init; }

    [Column("idDoc")]
public int idDoc { get; init; }

    [Column("cDescripcion")]
public string cDescripcion { get; init; }

    [Column("bEnviado")]
public bool bEnviado { get; init; }

    [Column("bFirmado")]
public bool bFirmado { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
