using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales.Dtos
{
    public class ColaboradorSucursalDtoResponse
    {


        public int ColaboradorSucursalId { get; set; }
        public ColaboradorDtoResponse Colaborador { get; set; } = null!;
        public SucursalDtoResponse Sucursal { get; set; } = null!;
        public decimal DistanciaKm { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

    }
}
