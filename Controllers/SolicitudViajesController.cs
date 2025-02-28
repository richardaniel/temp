using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudViajesController : ApiController
    {
        private readonly SolicitudViajeService _solicitudViajeService;

        public SolicitudViajesController(SolicitudViajeService solicitudViajeService)
        {
            _solicitudViajeService = solicitudViajeService;
        }

        [HttpPost("RegistrarSolicitudViaje")]

        public async Task<IActionResult> RegistrarSolicitudViaje([FromBody] SolicitudViajeDto request)
        {
            var registerSolicitudViajeResult = await _solicitudViajeService.RegistrarSolicitudViaje(request);
            return registerSolicitudViajeResult.Match(
                solicitudViaje => Ok(registerSolicitudViajeResult.Value),
                error => Problem(error)
            );
        }
    }
}
