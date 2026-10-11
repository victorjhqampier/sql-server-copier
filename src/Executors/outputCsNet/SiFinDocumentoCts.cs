using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_documento_cts", ExplicitColumns = true)]
internal sealed class SiFinDocumentoCts
{
    [PartitionKey(0)]
[Column("idDocumentoCTS")]
public int IdDocumentoCts { get; init; }

    [Column("cDocumentoCTS")]
public string CDocumentoCts { get; init; }

    [Column("idTipoDocumentoCTS")]
public int IdTipoDocumentoCts { get; init; }

    [Column("lEstado")]
public bool LEstado { get; init; }
}
