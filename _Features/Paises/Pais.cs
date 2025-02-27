using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Monedas;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;

public partial class Pais
{
    public int PaisId { get; set; }

    public string Nombre { get; set; } = null!;

    public string CodigoIso { get; set; } = null!;

    public virtual ICollection<Colaborador> Colaboradores { get; set; } = new List<Colaborador>();

    public virtual ICollection<Estado> Estados { get; set; } = new List<Estado>();

    public virtual ICollection<Moneda> Moneda { get; set; } = new List<Moneda>();

    public virtual ICollection<Sucursal> Sucursales { get; set; } = new List<Sucursal>();

    public virtual ICollection<Transportista> Transportista { get; set; } = new List<Transportista>();

    
}
