using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_limite_transferencia", ExplicitColumns = true)]
internal sealed class SiFinLimiteTransferencia
{
    [PartitionKey(0)]
[Column("idLimiteTransferencia")]
public int IdLimiteTransferencia { get; init; }

    [Column("idModulo")]
public int IdModulo { get; init; }

    [Column("idModalidadConsultaCCI")]
public int? IdModalidadConsultaCci { get; init; }

    [Column("idCanal")]
public int IdCanal { get; init; }

    [Column("idProducto")]
public int? IdProducto { get; init; }

    [Column("idTipoOperacion")]
public int? IdTipoOperacion { get; init; }

    [Column("idMoneda")]
public int? IdMoneda { get; init; }

    [Column("idDirectorioCCE")]
public int? IdDirectorioCce { get; init; }

    [Column("idTipoLimite")]
public int? IdTipoLimite { get; init; }

    [Column("idTipoEntidadTrans")]
public int? IdTipoEntidadTrans { get; init; }

    [Column("idPropiedadTitularTrans")]
public int? IdPropiedadTitularTrans { get; init; }

    [Column("nMontoMinimo")]
public decimal? NMontoMinimo { get; init; }

    [Column("nMontoMaximo")]
public decimal? NMontoMaximo { get; init; }

    [Column("lLimitePorDefecto")]
public bool? LLimitePorDefecto { get; init; }

    [Column("lVigente")]
public bool? LVigente { get; init; }

    [Column("dFechaInicio")]
public Cassandra.LocalDate DFechaInicio { get; init; }

    [Column("dFechaUpdate")]
public Cassandra.LocalDate? DFechaUpdate { get; init; }
}
