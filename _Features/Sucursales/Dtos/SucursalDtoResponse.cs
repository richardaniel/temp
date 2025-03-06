using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Estados.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales.Dtos
{
    public class SucursalDtoResponse
    {

        public int SucursalId { get; set; }
        public String Nombre { get; set; }
      
        public Decimal Latitud { get; set; }
        public Decimal Longitud { get; set; }

        public String DireccionExacta { get; set; }
        public PaisDto Pais { get; set; }
        public EstadoDto Estado { get; set; }
        public CiudadDto Ciudad { get; set; }
        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; }
    }
}
