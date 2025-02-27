
using System;
using System.Collections.Generic;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Notificaciones;

public partial class Notificacion
{
    public int NotificacionId { get; set; }

    public string ReceptorTipo { get; set; } = null!;

    public int? TransportistaId { get; set; }

    public int? UsuarioId { get; set; }

    public string Mensaje { get; set; } = null!;

    public DateTime? FechaNotificacion { get; set; }

    public virtual Transportista? Transportista { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
