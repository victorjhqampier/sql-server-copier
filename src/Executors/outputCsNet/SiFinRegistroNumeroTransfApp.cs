using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_registro_numero_transf_app", ExplicitColumns = true)]
internal sealed class SiFinRegistroNumeroTransfApp
{
    [PartitionKey(0)]
[Column("idRegistroNumeroTransfApp")]
public int idRegistroNumeroTransfApp { get; init; }

    [Column("cTelefonoApp")]
public string? cTelefonoApp { get; init; }

    [Column("idCli")]
public int? idCli { get; init; }

    [Column("idTipoDocumento")]
public int? idTipoDocumento { get; init; }

    [Column("cDocumentoID")]
public string? cDocumentoID { get; init; }

    [Column("idCanal")]
public int? idCanal { get; init; }

    [Column("idClienteApp")]
public string? idClienteApp { get; init; }

    [Column("idModulo")]
public int? idModulo { get; init; }

    [Column("dFechaRegistro")]
public System.DateTime? dFechaRegistro { get; init; }

    [Column("lTransfInteroperabilidad")]
public bool? lTransfInteroperabilidad { get; init; }

    [Column("idCuenta")]
public int? idCuenta { get; init; }

    [Column("cNumeroCCI")]
public string? cNumeroCCI { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }
}
