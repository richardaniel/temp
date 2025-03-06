using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales.Dtos
{
    public class ColaboradorAsignadoDto
    {
        public int ColaboradorId { get; set; }
        public decimal Distancia_vivienda { get; set; }

        public SucursalDto sucursal { get; set; }

        public ColaboradorDtoRequest colaborador {get;set;}

    }
}
