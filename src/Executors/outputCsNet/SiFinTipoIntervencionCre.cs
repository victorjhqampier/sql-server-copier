using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_tipo_intervencion_cre", ExplicitColumns = true)]
internal sealed class SiFinTipoIntervencionCre
{
    [PartitionKey(0)]
[Column("idTipoInterv")]
public int IdTipoInterv { get; init; }

    [Column("cTipoIntervencion")]
public string CTipoIntervencion { get; init; }

    [Column("lEstado")]
public bool? LEstado { get; init; }

    [Column("idModulo")]
public int? IdModulo { get; init; }
}
