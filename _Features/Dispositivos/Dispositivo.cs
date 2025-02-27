
using System;
using System.Collections.Generic;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Dispositivos;

public partial class Dispositivo
{
    public int DispositivoId { get; set; }

    public int UsuarioId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public bool Activo { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
