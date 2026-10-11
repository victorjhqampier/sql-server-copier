using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_bloqueo_cuenta", ExplicitColumns = true)]
internal sealed class SiFinBloqueoCuenta
{
    [PartitionKey(0)]
[Column("idBloCta")]
public int IdBloCta { get; init; }

    [Column("idCuenta")]
public int IdCuenta { get; init; }

    [Column("idBloqueo")]
public int IdBloqueo { get; init; }

    [Column("idCaracteristicaBloq")]
public int IdCaracteristicaBloq { get; init; }

    [Column("nMonBloqueo")]
public decimal NMonBloqueo { get; init; }

    [Column("cDesMotivo")]
public string CDesMotivo { get; init; }

    [Column("lBloqueado")]
public bool? LBloqueado { get; init; }

    [Column("idUsuBloq")]
public int? IdUsuBloq { get; init; }

    [Column("dFechaBloq")]
public Cassandra.LocalDate? DFechaBloq { get; init; }

    [Column("idUsuDesbloq")]
public int? IdUsuDesbloq { get; init; }

    [Column("dFechaDesbloq")]
public Cassandra.LocalDate? DFechaDesbloq { get; init; }

    [Column("idTipoSolicitante")]
public int? IdTipoSolicitante { get; init; }

    [Column("cNombreSolicitante")]
public string? CNombreSolicitante { get; init; }

    [Column("idTipoSolDesbloqueo")]
public int? IdTipoSolDesbloqueo { get; init; }

    [Column("cNombreSolDesbloqueo")]
public string? CNombreSolDesbloqueo { get; init; }

    [Column("lvigente")]
public bool Lvigente { get; init; }

    [Column("dFechaDocBloqueo")]
public Cassandra.LocalDate? DFechaDocBloqueo { get; init; }

    [Column("dFecHoraBloq")]
public System.DateTime? DFecHoraBloq { get; init; }

    [Column("cMotivoDesbloq")]
public string? CMotivoDesbloq { get; init; }

    [Column("dFecHoraDesbloq")]
public System.DateTime? DFecHoraDesbloq { get; init; }

    [Column("idSolicitud")]
public int? IdSolicitud { get; init; }
}
