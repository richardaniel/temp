using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajeController : ApiController
    {
        private readonly ViajeService _viajeService;

        public ViajeController(ViajeService viajeService)
        {
            _viajeService = viajeService;
        }

        [HttpPost("CrearViaje")]
        public async Task<IActionResult> CrearViaje([FromBody] ViajeDto viajeDto)
        {
            var crearViajeResult = await _viajeService.CrearViaje(viajeDto);
            return crearViajeResult.Match(
               colaboradorCreado => Ok(crearViajeResult.Value),
               errors => Problem(errors));

        }

        [HttpPost("ActualizarViaje")]
        public async Task<IActionResult> ActualizarVoaje([FromBody] DateTime Fecha)
        {
            var crearViajeResult = await _viajeService.ActualizarViajes(Fecha);
            return crearViajeResult.Match(
               colaboradorCreado => Ok(crearViajeResult.Value),
               errors => Problem(errors));
        }

        

    }
}
