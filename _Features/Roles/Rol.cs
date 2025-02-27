
using System;
using System.Collections.Generic;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Roles;

public partial class Rol
{
    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
