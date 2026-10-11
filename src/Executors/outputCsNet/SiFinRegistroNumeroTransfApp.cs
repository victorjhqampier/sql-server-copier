using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_registro_numero_transf_app", ExplicitColumns = true)]
internal sealed class SiFinRegistroNumeroTransfApp
{
    [PartitionKey(0)]
[Column("idRegistroNumeroTransfApp")]
public int IdRegistroNumeroTransfApp { get; init; }

    [Column("cTelefonoApp")]
public string? CTelefonoApp { get; init; }

    [Column("idCli")]
public int? IdCli { get; init; }

    [Column("idTipoDocumento")]
public int? IdTipoDocumento { get; init; }

    [Column("cDocumentoID")]
public string? CDocumentoId { get; init; }

    [Column("idCanal")]
public int? IdCanal { get; init; }

    [Column("idClienteApp")]
public string? IdClienteApp { get; init; }

    [Column("idModulo")]
public int? IdModulo { get; init; }

    [Column("dFechaRegistro")]
public System.DateTime? DFechaRegistro { get; init; }

    [Column("lTransfInteroperabilidad")]
public bool? LTransfInteroperabilidad { get; init; }

    [Column("idCuenta")]
public int? IdCuenta { get; init; }

    [Column("cNumeroCCI")]
public string? CNumeroCci { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
