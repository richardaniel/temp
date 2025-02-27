using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Estados.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales.Dtos
{
    public class SucursalDto
    {
        public int sucursalId { get; set; }
        public string Nombre { get; set; } 
        public string Direccion { get; set; }

        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public PaisDto Pais { get; set; }
        public EstadoDto Estado { get; set; }
        public  CiudadDto Ciudad { get; set; }


    }
}
