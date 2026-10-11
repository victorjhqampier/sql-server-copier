using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_documento_cts", ExplicitColumns = true)]
internal sealed class SiFinDocumentoCts
{
    [PartitionKey(0)]
[Column("idDocumentoCTS")]
public int idDocumentoCTS { get; init; }

    [Column("cDocumentoCTS")]
public string cDocumentoCTS { get; init; }

    [Column("idTipoDocumentoCTS")]
public int idTipoDocumentoCTS { get; init; }

    [Column("lEstado")]
public bool lEstado { get; init; }
}
