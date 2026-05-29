using System;
using System.Collections.Generic;

namespace PC1.DAW.CORE.Core.Entities;

public partial class TipoServicio
{
    public int ID_TS { get; set; }

    public string? Nombre { get; set; }

    public decimal? PrecioBase { get; set; }

    public virtual ICollection<OrdenServicio> OrdenServicios { get; set; } = new List<OrdenServicio>();
}
