using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_reg_documento_cts", ExplicitColumns = true)]
internal sealed class SiFinRegDocumentoCts
{
    [PartitionKey(0)]
[Column("idRegDoc")]
public int IdRegDoc { get; init; }

    [Column("idCuenta")]
public int IdCuenta { get; init; }

    [Column("idDoc")]
public int IdDoc { get; init; }

    [Column("cDescripcion")]
public string CDescripcion { get; init; }

    [Column("bEnviado")]
public bool BEnviado { get; init; }

    [Column("bFirmado")]
public bool BFirmado { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
