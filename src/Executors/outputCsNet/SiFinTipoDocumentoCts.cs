using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_documento_cts", ExplicitColumns = true)]
internal sealed class SiFinTipoDocumentoCts
{
    [PartitionKey(0)]
[Column("idTipoDocumentoCTS")]
public int IdTipoDocumentoCts { get; init; }

    [Column("cTipoDocumentoCTS")]
public string? CTipoDocumentoCts { get; init; }

    [Column("lEstado")]
public bool LEstado { get; init; }
}
