using System;
using System.Collections.Generic;

namespace PC1.DAW.CORE.Core.Entities;

public partial class Vehiculo
{
    public int ID_V { get; set; }

    public string? Placa { get; set; }

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public int? Año { get; set; }

    public int? ID_Cliente { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<OrdenServicio> OrdenServicios { get; set; } = new List<OrdenServicio>();
}
