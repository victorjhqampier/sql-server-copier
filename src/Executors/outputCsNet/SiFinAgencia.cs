using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_agencia", ExplicitColumns = true)]
internal sealed class SiFinAgencia
{
    [PartitionKey(0)]
[Column("idAgencia")]
public int idAgencia { get; init; }

    [Column("cNombreAge")]
public string cNombreAge { get; init; }

    [Column("cNomCorto")]
public string? cNomCorto { get; init; }

    [Column("idUbigeo")]
public int idUbigeo { get; init; }

    [Column("cDireccion")]
public string cDireccion { get; init; }

    [Column("cRefDirec")]
public string? cRefDirec { get; init; }

    [Column("cFono")]
public string? cFono { get; init; }

    [Column("lEstado")]
public bool? lEstado { get; init; }

    [Column("cCodSBS")]
public string? cCodSBS { get; init; }

    [Column("idTipoOficina")]
public int idTipoOficina { get; init; }

    [Column("dfechaCreacion")]
public System.DateTime? dfechaCreacion { get; init; }

    [Column("cNroResolucion")]
public string? cNroResolucion { get; init; }

    [Column("idTipEsquemaCaja")]
public int? idTipEsquemaCaja { get; init; }

    [Column("nAnioResolucion")]
public int? nAnioResolucion { get; init; }

    [Column("lAplicaGiros")]
public bool? lAplicaGiros { get; init; }

    [Column("idCategoria")]
public int? idCategoria { get; init; }
}
