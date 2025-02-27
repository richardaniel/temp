using Richar.Academia.ProyectoFinal.WebAPI._Features.Evaluaciones;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Monedas;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;


namespace Richar.Academia.ProyectoFinal.WebAPI._Features;

public partial class Viaje
{
    public int ViajeId { get; set; }

    public int SucursalId { get; set; }

    public int TransportistaId { get; set; }

    public DateTime FechaViaje { get; set; }

    public int UsuarioRegistroId { get; set; }

    public bool Activo { get; set; }

    public decimal DistanciaTotalKm { get; set; }

    public decimal CostoTotal { get; set; }

    public int MonedaId { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public virtual ICollection<DetalleViaje> DetalleViajes { get; set; } = new List<DetalleViaje>();

    public virtual ICollection<Evaluacion> Evaluaciones { get; set; } = new List<Evaluacion>();

    public virtual Moneda Moneda { get; set; } = null!;

    public virtual Sucursal Sucursal { get; set; } = null!;

    public virtual Transportista Transportista { get; set; } = null!;

    public virtual Usuario UsuarioRegistro { get; set; } = null!;
}
