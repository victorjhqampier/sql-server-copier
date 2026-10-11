using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_agencia_cce", ExplicitColumns = true)]
internal sealed class SiFinAgenciaCce
{
    [PartitionKey(0)]
[Column("idAgenciaCCE")]
public int idAgenciaCCE { get; init; }

    [Column("cCodigoRegistro")]
public string? cCodigoRegistro { get; init; }

    [Column("cNumeroEntidad")]
public string? cNumeroEntidad { get; init; }

    [Column("cNumeroOficina")]
public string? cNumeroOficina { get; init; }

    [Column("cNumeroOficinaPlaza")]
public string? cNumeroOficinaPlaza { get; init; }

    [Column("cUbigeoPlazaCAnexo")]
public string? cUbigeoPlazaCAnexo { get; init; }

    [Column("cUbigeoPlazaCTransf")]
public string? cUbigeoPlazaCTransf { get; init; }

    [Column("cNombreOficina")]
public string? cNombreOficina { get; init; }

    [Column("cNombreDomicilioOfi")]
public string? cNombreDomicilioOfi { get; init; }

    [Column("cNombreLocalidad")]
public string? cNombreLocalidad { get; init; }

    [Column("cCodigoPostDistrito")]
public string? cCodigoPostDistrito { get; init; }

    [Column("cNombrePlaza")]
public string? cNombrePlaza { get; init; }

    [Column("cTipoOficina")]
public string? cTipoOficina { get; init; }

    [Column("cUbigeoINEI")]
public string? cUbigeoINEI { get; init; }

    [Column("cPlazaExclusiva")]
public string? cPlazaExclusiva { get; init; }

    [Column("cPrefTelef")]
public string? cPrefTelef { get; init; }

    [Column("cNumTelef1")]
public string? cNumTelef1 { get; init; }

    [Column("cNumTelef2")]
public string? cNumTelef2 { get; init; }

    [Column("cNumTelefFax")]
public string? cNumTelefFax { get; init; }

    [Column("cNumTelefCentral")]
public string? cNumTelefCentral { get; init; }

    [Column("cNumExtension")]
public string? cNumExtension { get; init; }

    [Column("cMotivoActualizacion")]
public string? cMotivoActualizacion { get; init; }

    [Column("cFechaActualizacion")]
public string? cFechaActualizacion { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
