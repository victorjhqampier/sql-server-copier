using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_est_consulta_cce", ExplicitColumns = true)]
internal sealed class SiFinEstConsultaCce
{
    [PartitionKey(0)]
[Column("idEstConsultaCCE")]
public int IdEstConsultaCce { get; init; }

    [Column("idTipoOperacion")]
public int? IdTipoOperacion { get; init; }

    [Column("idEntidadOperacion")]
public int? IdEntidadOperacion { get; init; }

    [Column("idAmbitoTransferencia")]
public int? IdAmbitoTransferencia { get; init; }

    [Column("idModalidadConsultaCCI")]
public int? IdModalidadConsultaCci { get; init; }

    [Column("cNumeroCCIOrigen")]
public string? CNumeroCciOrigen { get; init; }

    [Column("cNumeroCCIDestino")]
public string? CNumeroCciDestino { get; init; }

    [Column("cCodigoOperacion")]
public string? CCodigoOperacion { get; init; }

    [Column("cNumeroCelDestino")]
public string? CNumeroCelDestino { get; init; }

    [Column("lRechazadoCCE")]
public bool? LRechazadoCce { get; init; }

    [Column("cCodigoRechazado")]
public string? CCodigoRechazado { get; init; }

    [Column("cCodigoEstado")]
public string? CCodigoEstado { get; init; }

    [Column("dFechaHoraOpe")]
public System.DateTime? DFechaHoraOpe { get; init; }

    [Column("idCanal")]
public int? IdCanal { get; init; }

    [Column("cDocumentoID")]
public string? CDocumentoId { get; init; }

    [Column("idMoneda")]
public int? IdMoneda { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }
}
