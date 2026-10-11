using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_concepto", ExplicitColumns = true)]
internal sealed class SiFinConcepto
{
    [PartitionKey(0)]
[Column("idConcepto")]
public int idConcepto { get; init; }

    [Column("cConcepto")]
public string? cConcepto { get; init; }

    [Column("lVigente")]
public bool lVigente { get; init; }

    [Column("cNombreCorto")]
public string? cNombreCorto { get; init; }

    [Column("idGrupoCon")]
public int? idGrupoCon { get; init; }

    [Column("lAplicaContable")]
public bool? lAplicaContable { get; init; }

    [Column("nOrden")]
public int? nOrden { get; init; }
}
