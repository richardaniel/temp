
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes;
using Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Roles.Dtos;



namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprobacionSolicitudController : ApiController
    {
        private readonly AprobacionSolicitudService _aprobacionSolicitudService;

        public AprobacionSolicitudController(AprobacionSolicitudService aprobacionSolicitudService)
        {
            _aprobacionSolicitudService = aprobacionSolicitudService;
        }

        //[Authorize]
        [HttpPost("GestionarSolicitudViaje")]
        public async Task<IActionResult> GestionarSolicitudViaje([FromBody]AprobacionSolicitudDto request)
        {
           var gestionarSolicitudViajeResult = await _aprobacionSolicitudService.GestionarSolicitudViaje(request);
            
           return gestionarSolicitudViajeResult.Match(
               solicitudViaje => Ok(gestionarSolicitudViajeResult.Value),
               error => Problem(error)
           );

        }

       // [Authorize]
        [HttpPost("GestionarSolicitudesViaje")]
        public async Task<IActionResult> GestionarSolicitudesViaje([FromBody] List<AprobacionSolicitudDto> request)
        {
            var gestionarSolicitudesViajeResult = await _aprobacionSolicitudService.GestionarSolicitudesViaje(request);

            return gestionarSolicitudesViajeResult.Match(
                solicitudViaje => Ok(gestionarSolicitudesViajeResult),
                error => Problem(error)
            );
        }
    }
}
