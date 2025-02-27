using Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.EstadoSolicitudes;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;


namespace Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje;

public partial class SolicitudViaje
{
    public int SolicitudViajeId { get; set; }

    public int ColaboradorId { get; set; }

    public int EstadoSolicitudId { get; set; }

    public int SucursalId { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public string? Comentario { get; set; }

    public virtual ICollection<AprobacionesSolicitud> AprobacionesSolicitudes { get; set; } = new List<AprobacionesSolicitud>();

    public virtual Colaborador Colaborador { get; set; } = null!;

    public virtual EstadoSolicitud EstadoSolicitud { get; set; } = null!;

    public virtual Sucursal Sucursal { get; set; } = null!;
}
