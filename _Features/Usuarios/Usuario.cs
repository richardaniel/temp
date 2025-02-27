using Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Dispositivos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Notificaciones;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Roles;
using System;
using System.Collections.Generic;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public byte[] Contrasena { get; set; } = null!;

    public int RolId { get; set; }

    public int? ColaboradorId { get; set; }

    public bool Activo { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public virtual ICollection<AprobacionesSolicitud> AprobacionesSolicitudes { get; set; } = new List<AprobacionesSolicitud>();

    public virtual Colaborador? Colaborador { get; set; }

    public virtual ICollection<Dispositivo> Dispositivos { get; set; } = new List<Dispositivo>();

    public virtual ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();

    public virtual Rol Rol { get; set; } = null!;

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
