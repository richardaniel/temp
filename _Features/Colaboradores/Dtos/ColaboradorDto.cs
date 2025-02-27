using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Estados.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos
{
    public class ColaboradorDto
    {
        public ColaboradorDto() { }
        
        public int ColaboradorId { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Email { get; set; }
        public String Telefono { get; set; }
        public Decimal Latitud { get; set; }
        public Decimal Longitud { get; set; }
        public PaisDto Pais { get; set; }
        public EstadoDto Estado { get; set; }
        public CiudadDto Ciudad { get; set; }
        public bool Activo { get; set; } = true;

       
    }
}
