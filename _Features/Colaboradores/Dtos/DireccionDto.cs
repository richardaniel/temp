using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Estados.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos
{
    public class DireccionDto
    {
        PaisDto pais { get; set; }
        EstadoDto estado { get; set; }
        CiudadDto ciudad { get; set; }
    }
}
