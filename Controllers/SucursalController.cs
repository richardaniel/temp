using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales.Dtos;
using ErrorOr;

namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{

    [Route("api/[controller]")]
    public class SucursalController:ApiController
    {

        private readonly SucursalService _sucursalService;

        public SucursalController(SucursalService sucursalService)
        {
            _sucursalService = sucursalService;
        }

        [HttpPost("RegistrarSucursal")]
        public async Task<IActionResult> RegistrarSucursal([FromBody] SucursalDto request)
        {

            var registerSucursalResult =  await _sucursalService.RegistrarSucursal(request);
            return registerSucursalResult.Match(
                sucursal => Ok(registerSucursalResult),
                error =>Problem(error)
            );
        }

        [HttpGet("ObtenerSucursales")]
        public Task<IActionResult> ObtenerSucursales()
        {
            var obtenerSucursalesResult = _sucursalService.ObtenerSucursales();
            return obtenerSucursalesResult.Match(
                 sucursalesObtenidas => Ok(obtenerSucursalesResult.Result.Value),
                 error => Problem(error)
                 );
                
        }
    }
}
