using System;
using System.Collections.Generic;

namespace PC1.DAW.CORE.Core.Entities;

public partial class OrdenServicio
{
    public int ID_OS { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public string? DescripcionProblema { get; set; }

    public decimal? CostoEstimado { get; set; }

    public string? Estado { get; set; }

    public int? ID_V { get; set; }

    public int? ID_TS { get; set; }

    public virtual Vehiculo? Vehiculo { get; set; }

    public virtual TipoServicio? TipoServicio { get; set; }
}
