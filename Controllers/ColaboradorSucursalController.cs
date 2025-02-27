using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales;
using Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColaboradorSucursalController : ApiController
    {

        private readonly ColaboradorSucursalService _colaboradorSucursalService;

        public ColaboradorSucursalController(ColaboradorSucursalService colaboradorSucursalService)
        {
            _colaboradorSucursalService = colaboradorSucursalService;
        }

        [HttpPost("AsignarColaboradorASucursal")]
        public  async Task<IActionResult> AsignarColaboradorASucursal([FromBody] ColaboradorSucursalDto colaboradorSucursalDto)
        {

            var resultAsignarColaborador = await _colaboradorSucursalService.AsignarColaboradorASucursal(colaboradorSucursalDto);
            return resultAsignarColaborador.Match(
                colaboradorAsignado => Ok(resultAsignarColaborador.Value),
                errors =>Problem()
                );
        }
    }
}
