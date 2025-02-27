using System;
using System.Collections.Generic;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features;

public class DetalleViaje
{
    public int DetalleViajeId { get; set; }

    public int ViajeId { get; set; }

    public int ColaboradorSucursalId { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public virtual ColaboradorSucursal ColaboradorSucursal { get; set; } = null!;

    public virtual Viaje Viaje { get; set; } = null!;
}
