using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_agencia", ExplicitColumns = true)]
internal sealed class SiFinAgencia
{
    [PartitionKey(0)]
[Column("idAgencia")]
public int IdAgencia { get; init; }

    [Column("cNombreAge")]
public string CNombreAge { get; init; }

    [Column("cNomCorto")]
public string? CNomCorto { get; init; }

    [Column("idUbigeo")]
public int IdUbigeo { get; init; }

    [Column("cDirección")]
public string CDirecciN { get; init; }

    [Column("cRefDirec")]
public string? CRefDirec { get; init; }

    [Column("cFono")]
public string? CFono { get; init; }

    [Column("lEstado")]
public bool? LEstado { get; init; }

    [Column("cCodSBS")]
public string? CCodSbs { get; init; }

    [Column("idTipoOficina")]
public int IdTipoOficina { get; init; }

    [Column("dfechaCreacion")]
public System.DateTime? DfechaCreacion { get; init; }

    [Column("cNroResolucion")]
public string? CNroResolucion { get; init; }

    [Column("idTipEsquemaCaja")]
public int? IdTipEsquemaCaja { get; init; }

    [Column("nAnioResolucion")]
public int? NAnioResolucion { get; init; }

    [Column("lAplicaGiros")]
public bool? LAplicaGiros { get; init; }

    [Column("idCategoria")]
public int? IdCategoria { get; init; }
}
