using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Dispositivos;

namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispositivosController : ApiController
    {

        private readonly DispositivoService _dispositivosService;

        public DispositivosController(DispositivoService dispositivoService)
        {
            _dispositivosService = dispositivoService;
        }

        [HttpPost("crearDispositivo")]
        public async Task<IActionResult> CrearDispositivo([FromBody] string token)
        {
            var crearDispositivoResult = await _dispositivosService.RegistrarDispositivo(token);
            return crearDispositivoResult.Match(
                dispositivoCreado => Ok(crearDispositivoResult.Value),
                errors => Problem(errors));
        }

    }
}
