using Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje;
using System;
using System.Collections.Generic;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.EstadoSolicitudes;

public partial class EstadoSolicitud
{
    public int EstadoSolicitudId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<AprobacionesSolicitud> AprobacionesSolicitudes { get; set; } = new List<AprobacionesSolicitud>();

    public virtual ICollection<SolicitudViaje> SolicitudesViajes { get; set; } = new List<SolicitudViaje>();
}
