using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_modalidad_consulta_cci", ExplicitColumns = true)]
internal sealed class SiFinModalidadConsultaCci
{
    [PartitionKey(0)]
[Column("idModalidadConsultaCCI")]
public int idModalidadConsultaCCI { get; init; }

    [Column("cModalidadConsultaCCI")]
public string? cModalidadConsultaCCI { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
