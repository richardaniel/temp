using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;


namespace Richar.Academia.ProyectoFinal.WebAPI._Features;

public partial class ColaboradorSucursal
{
    public int ColaboradorSucursalId { get; set; }

    public int ColaboradorId { get; set; }

    public int SucursalId { get; set; }

    public decimal DistanciaKm { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public virtual Colaborador Colaborador { get; set; } = null!;

    public virtual ICollection<DetalleViaje> DetalleViajes { get; set; } = new List<DetalleViaje>();

    public virtual Sucursal Sucursal { get; set; } = null!;
}
