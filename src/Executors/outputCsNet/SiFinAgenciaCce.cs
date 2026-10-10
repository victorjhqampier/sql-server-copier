using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_agencia_cce", ExplicitColumns = true)]
internal sealed class SiFinAgenciaCce
{
    [PartitionKey(0)]
[Column("idAgenciaCCE")]
public int IdAgenciaCce { get; init; }

    [Column("cCodigoRegistro")]
public string? CCodigoRegistro { get; init; }

    [Column("cNumeroEntidad")]
public string? CNumeroEntidad { get; init; }

    [Column("cNumeroOficina")]
public string? CNumeroOficina { get; init; }

    [Column("cNumeroOficinaPlaza")]
public string? CNumeroOficinaPlaza { get; init; }

    [Column("cUbigeoPlazaCAnexo")]
public string? CUbigeoPlazaCAnexo { get; init; }

    [Column("cUbigeoPlazaCTransf")]
public string? CUbigeoPlazaCTransf { get; init; }

    [Column("cNombreOficina")]
public string? CNombreOficina { get; init; }

    [Column("cNombreDomicilioOfi")]
public string? CNombreDomicilioOfi { get; init; }

    [Column("cNombreLocalidad")]
public string? CNombreLocalidad { get; init; }

    [Column("cCodigoPostDistrito")]
public string? CCodigoPostDistrito { get; init; }

    [Column("cNombrePlaza")]
public string? CNombrePlaza { get; init; }

    [Column("cTipoOficina")]
public string? CTipoOficina { get; init; }

    [Column("cUbigeoINEI")]
public string? CUbigeoInei { get; init; }

    [Column("cPlazaExclusiva")]
public string? CPlazaExclusiva { get; init; }

    [Column("cPrefTelef")]
public string? CPrefTelef { get; init; }

    [Column("cNumTelef1")]
public string? CNumTelef1 { get; init; }

    [Column("cNumTelef2")]
public string? CNumTelef2 { get; init; }

    [Column("cNumTelefFax")]
public string? CNumTelefFax { get; init; }

    [Column("cNumTelefCentral")]
public string? CNumTelefCentral { get; init; }

    [Column("cNumExtension")]
public string? CNumExtension { get; init; }

    [Column("cMotivoActualizacion")]
public string? CMotivoActualizacion { get; init; }

    [Column("cFechaActualizacion")]
public string? CFechaActualizacion { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
