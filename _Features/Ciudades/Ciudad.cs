using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;

using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;


namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;

public partial class Ciudad
{
    public int CiudadId { get; set; }

    public string Nombre { get; set; } = null!;

    public int EstadoId { get; set; }

    public virtual ICollection<Colaborador> Colaboradores { get; set; } = new List<Colaborador>();

    public virtual Estado Estado { get; set; } = null!;

    public virtual ICollection<Sucursal> Sucursales { get; set; } = new List<Sucursal>();

    public virtual ICollection<Transportista> Transportista { get; set; } = new List<Transportista>();
}
