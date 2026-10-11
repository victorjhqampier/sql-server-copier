using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_documento_cts", ExplicitColumns = true)]
internal sealed class SiFinTipoDocumentoCts
{
    [PartitionKey(0)]
[Column("idTipoDocumentoCTS")]
public int idTipoDocumentoCTS { get; init; }

    [Column("cTipoDocumentoCTS")]
public string? cTipoDocumentoCTS { get; init; }

    [Column("lEstado")]
public bool lEstado { get; init; }
}
