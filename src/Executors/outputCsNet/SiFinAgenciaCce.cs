using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_agencia_cce", ExplicitColumns = true)]
internal sealed class SiFinAgenciaCce
{
    [PartitionKey(0)]
    [Column("idAgenciaCCE")]
    public int IdAgenciaCce { get; init; }

    [Column("cCodigoRegistro")]
    public string? CodigoRegistro { get; init; }

    [Column("cNumeroEntidad")]
    public string? NumeroEntidad { get; init; }

    [Column("cNumeroOficina")]
    public string? NumeroOficina { get; init; }

    [Column("cNumeroOficinaPlaza")]
    public string? NumeroOficinaPlaza { get; init; }

    [Column("cUbigeoPlazaCAnexo")]
    public string? UbigeoPlazaCAnexo { get; init; }

    [Column("cUbigeoPlazaCTransf")]
    public string? UbigeoPlazaCTransf { get; init; }

    [Column("cNombreOficina")]
    public string? NombreOficina { get; init; }

    [Column("cNombreDomicilioOfi")]
    public string? NombreDomicilioOfi { get; init; }

    [Column("cNombreLocalidad")]
    public string? NombreLocalidad { get; init; }

    [Column("cCodigoPostDistrito")]
    public string? CodigoPostDistrito { get; init; }

    [Column("cNombrePlaza")]
    public string? NombrePlaza { get; init; }

    [Column("cTipoOficina")]
    public string? TipoOficina { get; init; }

    [Column("cUbigeoINEI")]
    public string? UbigeoInei { get; init; }

    [Column("cPlazaExclusiva")]
    public string? PlazaExclusiva { get; init; }

    [Column("cPrefTelef")]
    public string? PrefTelef { get; init; }

    [Column("cNumTelef1")]
    public string? NumTelef1 { get; init; }

    [Column("cNumTelef2")]
    public string? NumTelef2 { get; init; }

    [Column("cNumTelefFax")]
    public string? NumTelefFax { get; init; }

    [Column("cNumTelefCentral")]
    public string? NumTelefCentral { get; init; }

    [Column("cNumExtension")]
    public string? NumExtension { get; init; }

    [Column("cMotivoActualizacion")]
    public string? MotivoActualizacion { get; init; }

    [Column("cFechaActualizacion")]
    public string? FechaActualizacion { get; init; }

    [Column("lVigente")]
    public bool Vigente { get; init; }
}