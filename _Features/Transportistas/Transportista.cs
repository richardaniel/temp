using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Monedas;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Notificaciones;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;


namespace Richar.Academia.ProyectoFinal.WebAPI._Features;

public partial class Transportista
{
    public int TransportistaId { get; set; }

    public string Nombre { get; set; } = null!;

    public int MonedaId { get; set; }

    public decimal TarifaKm { get; set; }

    public bool Activo { get; set; }

    public string Email { get; set; } = null!;

    public int PaisId { get; set; }

    public int EstadoId { get; set; }

    public int CiudadId { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public virtual Ciudad Ciudad { get; set; } = null!;

    public virtual Estado Estado { get; set; } = null!;

    public virtual Moneda Moneda { get; set; } = null!;

    public virtual ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();

    public virtual Pais Pais { get; set; } = null!;

    public virtual ICollection<Viaje> Viajes { get; set; } = new List<Viaje>();
}
