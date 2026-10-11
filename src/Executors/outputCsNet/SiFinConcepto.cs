using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_concepto", ExplicitColumns = true)]
internal sealed class SiFinConcepto
{
    [PartitionKey(0)]
[Column("idConcepto")]
public int IdConcepto { get; init; }

    [Column("cConcepto")]
public string? CConcepto { get; init; }

    [Column("lVigente")]
public bool LVigente { get; init; }

    [Column("cNombreCorto")]
public string? CNombreCorto { get; init; }

    [Column("idGrupoCon")]
public int? IdGrupoCon { get; init; }

    [Column("lAplicaContable")]
public bool? LAplicaContable { get; init; }

    [Column("nOrden")]
public int? NOrden { get; init; }
}
