using Cassandra.Mapping.Attributes;

namespace ScylladbInfrastructure.Collections.Tables;

[Table("si_fin_limite_transferencia", ExplicitColumns = true)]
internal sealed class SiFinLimiteTransferencia
{
    [PartitionKey(0)]
[Column("idLimiteTransferencia")]
public int idLimiteTransferencia { get; init; }

    [Column("idModulo")]
public int idModulo { get; init; }

    [Column("idModalidadConsultaCCI")]
public int? idModalidadConsultaCCI { get; init; }

    [Column("idCanal")]
public int idCanal { get; init; }

    [Column("idProducto")]
public int? idProducto { get; init; }

    [Column("idTipoOperacion")]
public int? idTipoOperacion { get; init; }

    [Column("idMoneda")]
public int? idMoneda { get; init; }

    [Column("idDirectorioCCE")]
public int? idDirectorioCCE { get; init; }

    [Column("idTipoLimite")]
public int? idTipoLimite { get; init; }

    [Column("idTipoEntidadTrans")]
public int? idTipoEntidadTrans { get; init; }

    [Column("idPropiedadTitularTrans")]
public int? idPropiedadTitularTrans { get; init; }

    [Column("nMontoMinimo")]
public decimal? nMontoMinimo { get; init; }

    [Column("nMontoMaximo")]
public decimal? nMontoMaximo { get; init; }

    [Column("lLimitePorDefecto")]
public bool? lLimitePorDefecto { get; init; }

    [Column("lVigente")]
public bool? lVigente { get; init; }

    [Column("dFechaInicio")]
public Cassandra.LocalDate dFechaInicio { get; init; }

    [Column("dFechaUpdate")]
public Cassandra.LocalDate? dFechaUpdate { get; init; }
}
