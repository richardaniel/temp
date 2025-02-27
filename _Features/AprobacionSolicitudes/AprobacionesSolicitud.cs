using Richar.Academia.ProyectoFinal.WebAPI._Features.EstadoSolicitudes;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje;

using System;
using System.Collections.Generic;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes;

public partial class AprobacionesSolicitud
{
    public int AprobacionSolicitudId { get; set; }

    public int SolicitudId { get; set; }

    public int GerenteId { get; set; }

    public DateTime? FechaAprobacion { get; set; }

    public int EstadoSolicitudId { get; set; }

    public string? Comentario { get; set; }

    public virtual EstadoSolicitud EstadoSolicitud { get; set; } = null!;

    public virtual Usuario Gerente { get; set; } = null!;

    public virtual SolicitudViaje Solicitud { get; set; } = null!;
}
