using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Estados.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features;

public class Estado
{
    public int EstadoId { get; set; }
    public string Nombre { get; set; } = null!;

    public int PaisId { get; set; }

    public virtual ICollection<Ciudad> Ciudades { get; set; } = new List<Ciudad>();

    public virtual ICollection<Colaborador> Colaboradores { get; set; } = new List<Colaborador>();

    public virtual Pais Pais { get; set; } = null!;

    public virtual ICollection<Sucursal> Sucursales { get; set; } = new List<Sucursal>();

    public virtual ICollection<Transportista> Transportista { get; set; } = new List<Transportista>();

   
}
