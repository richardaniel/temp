using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Transportistas;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Transportistas.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportistaController : ApiController
    {

        private TransportistaService _transportistaService;

        public TransportistaController(TransportistaService transportistaService)
        {
            _transportistaService = transportistaService;
        }

        [HttpPost("RegistrarTransportista")]
        public async Task<IActionResult> RegistrarColaborador([FromBody] TransportistaDto request)
        {
            var result = await _transportistaService.RegistrarTransportista(request);
            return result.Match(
                success => Ok(result),
                error => Problem(error)
            );
        }

    }
}
