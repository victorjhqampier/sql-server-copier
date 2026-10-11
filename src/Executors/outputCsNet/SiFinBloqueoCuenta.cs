using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_bloqueo_cuenta", ExplicitColumns = true)]
internal sealed class SiFinBloqueoCuenta
{
    [PartitionKey(0)]
[Column("idBloCta")]
public int idBloCta { get; init; }

    [Column("idCuenta")]
public int idCuenta { get; init; }

    [Column("idBloqueo")]
public int idBloqueo { get; init; }

    [Column("idCaracteristicaBloq")]
public int idCaracteristicaBloq { get; init; }

    [Column("nMonBloqueo")]
public decimal nMonBloqueo { get; init; }

    [Column("cDesMotivo")]
public string cDesMotivo { get; init; }

    [Column("lBloqueado")]
public bool? lBloqueado { get; init; }

    [Column("idUsuBloq")]
public int? idUsuBloq { get; init; }

    [Column("dFechaBloq")]
public Cassandra.LocalDate? dFechaBloq { get; init; }

    [Column("idUsuDesbloq")]
public int? idUsuDesbloq { get; init; }

    [Column("dFechaDesbloq")]
public Cassandra.LocalDate? dFechaDesbloq { get; init; }

    [Column("idTipoSolicitante")]
public int? idTipoSolicitante { get; init; }

    [Column("cNombreSolicitante")]
public string? cNombreSolicitante { get; init; }

    [Column("idTipoSolDesbloqueo")]
public int? idTipoSolDesbloqueo { get; init; }

    [Column("cNombreSolDesbloqueo")]
public string? cNombreSolDesbloqueo { get; init; }

    [Column("lvigente")]
public bool lvigente { get; init; }

    [Column("dFechaDocBloqueo")]
public Cassandra.LocalDate? dFechaDocBloqueo { get; init; }

    [Column("dFecHoraBloq")]
public System.DateTime? dFecHoraBloq { get; init; }

    [Column("cMotivoDesbloq")]
public string? cMotivoDesbloq { get; init; }

    [Column("dFecHoraDesbloq")]
public System.DateTime? dFecHoraDesbloq { get; init; }

    [Column("idSolicitud")]
public int? idSolicitud { get; init; }
}
