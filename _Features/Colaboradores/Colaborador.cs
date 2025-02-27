using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Evaluaciones;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje;
using System;
using System.Collections.Generic;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;

public partial class Colaborador
{
    public int ColaboradorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefono { get; set; }

    public decimal Latitud { get; set; }

    public decimal Longitud { get; set; }

    public bool Activo { get; set; }

    public int PaisId { get; set; }

    public int EstadoId { get; set; }

    public int CiudadId { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public virtual Ciudad Ciudad { get; set; } = null!;

    public virtual ICollection<ColaboradorSucursal> ColaboradorSucursals { get; set; } = new List<ColaboradorSucursal>();

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<Evaluacion> Evaluaciones { get; set; } = new List<Evaluacion>();

    public virtual Pais Pais { get; set; } = null!;

    public virtual ICollection<SolicitudViaje> SolicitudesViajes { get; set; } = new List<SolicitudViaje>();

    public virtual Usuario? Usuario { get; set; }
}
