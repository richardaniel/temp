using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;

using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje;


namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;

public partial class Sucursal
{
    public int SucursalId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public decimal Latitud { get; set; }

    public decimal Longitud { get; set; }

    public int PaisId { get; set; }

    public int EstadoId { get; set; }

    public int CiudadId { get; set; }

    public bool Activo { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public virtual Ciudad Ciudad { get; set; } = null!;

    public virtual ICollection<ColaboradorSucursal> ColaboradorSucursals { get; set; } = new List<ColaboradorSucursal>();

    public virtual Estado Estado { get; set; } = null!;

    public virtual Pais Pais { get; set; } = null!;

    public virtual ICollection<SolicitudViaje> SolicitudesViajes { get; set; } = new List<SolicitudViaje>();

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
