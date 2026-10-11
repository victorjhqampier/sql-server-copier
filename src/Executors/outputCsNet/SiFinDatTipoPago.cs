using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_dat_tipo_pago", ExplicitColumns = true)]
internal sealed class SiFinDatTipoPago
{
    [PartitionKey(0)]
[Column("idkardex")]
public int idkardex { get; init; }

    [PartitionKey(1)]
[Column("idTipoValorado")]
public int idTipoValorado { get; init; }

    [PartitionKey(2)]
[Column("idCuenta")]
public int idCuenta { get; init; }

    [Column("cNroCuentaDoc")]
public string? cNroCuentaDoc { get; init; }

    [Column("cNroDocumento")]
public string? cNroDocumento { get; init; }

    [Column("cSerieDocumento")]
public string? cSerieDocumento { get; init; }

    [Column("idEntidad")]
public int idEntidad { get; init; }

    [Column("dFechaReg")]
public Cassandra.LocalDate? dFechaReg { get; init; }

    [Column("nDiasValoriz")]
public int nDiasValoriz { get; init; }

    [Column("nEstadoValoriz")]
public int nEstadoValoriz { get; init; }

    [Column("idAgencia")]
public int? idAgencia { get; init; }

    [Column("idUsuarioVal")]
public int? idUsuarioVal { get; init; }

    [Column("dFechaVal")]
public Cassandra.LocalDate? dFechaVal { get; init; }

    [Column("cMotivo")]
public string? cMotivo { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }

    [Column("idCuentaRel")]
public int? idCuentaRel { get; init; }

    [Column("idMoneda")]
public int? idMoneda { get; init; }

    [Column("nMontoDoc")]
public decimal? nMontoDoc { get; init; }

    [Column("dFechaEmiDoc")]
public Cassandra.LocalDate? dFechaEmiDoc { get; init; }

    [Column("nNroFolio")]
public int? nNroFolio { get; init; }

    [Column("idEntidadDestino")]
public int? idEntidadDestino { get; init; }

    [Column("cNroCtaDestino")]
public string? cNroCtaDestino { get; init; }

    [Column("lisMismaCta")]
public bool? lisMismaCta { get; init; }

    [Column("dFecHorReg")]
public System.DateTime? dFecHorReg { get; init; }

    [Column("idUsuActDatos")]
public int? idUsuActDatos { get; init; }

    [Column("dFechaActualiza")]
public Cassandra.LocalDate? dFechaActualiza { get; init; }

    [Column("dFecHoraActualiza")]
public System.DateTime? dFecHoraActualiza { get; init; }

    [Column("lIndicaActDatos")]
public bool? lIndicaActDatos { get; init; }
}
