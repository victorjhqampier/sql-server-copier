using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_dat_tipo_pago", ExplicitColumns = true)]
internal sealed class SiFinDatTipoPago
{
    [PartitionKey(0)]
[Column("idkardex")]
public int Idkardex { get; init; }

    [PartitionKey(1)]
[Column("idTipoValorado")]
public int IdTipoValorado { get; init; }

    [PartitionKey(2)]
[Column("idCuenta")]
public int IdCuenta { get; init; }

    [Column("cNroCuentaDoc")]
public string? CNroCuentaDoc { get; init; }

    [Column("cNroDocumento")]
public string? CNroDocumento { get; init; }

    [Column("cSerieDocumento")]
public string? CSerieDocumento { get; init; }

    [Column("idEntidad")]
public int IdEntidad { get; init; }

    [Column("dFechaReg")]
public Cassandra.LocalDate? DFechaReg { get; init; }

    [Column("nDiasValoriz")]
public int NDiasValoriz { get; init; }

    [Column("nEstadoValoriz")]
public int NEstadoValoriz { get; init; }

    [Column("idAgencia")]
public int? IdAgencia { get; init; }

    [Column("idUsuarioVal")]
public int? IdUsuarioVal { get; init; }

    [Column("dFechaVal")]
public Cassandra.LocalDate? DFechaVal { get; init; }

    [Column("cMotivo")]
public string? CMotivo { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }

    [Column("idCuentaRel")]
public int? IdCuentaRel { get; init; }

    [Column("idMoneda")]
public int? IdMoneda { get; init; }

    [Column("nMontoDoc")]
public decimal? NMontoDoc { get; init; }

    [Column("dFechaEmiDoc")]
public Cassandra.LocalDate? DFechaEmiDoc { get; init; }

    [Column("nNroFolio")]
public int? NNroFolio { get; init; }

    [Column("idEntidadDestino")]
public int? IdEntidadDestino { get; init; }

    [Column("cNroCtaDestino")]
public string? CNroCtaDestino { get; init; }

    [Column("lisMismaCta")]
public bool? LisMismaCta { get; init; }

    [Column("dFecHorReg")]
public System.DateTime? DFecHorReg { get; init; }

    [Column("idUsuActDatos")]
public int? IdUsuActDatos { get; init; }

    [Column("dFechaActualiza")]
public Cassandra.LocalDate? DFechaActualiza { get; init; }

    [Column("dFecHoraActualiza")]
public System.DateTime? DFecHoraActualiza { get; init; }

    [Column("lIndicaActDatos")]
public bool? LIndicaActDatos { get; init; }
}
