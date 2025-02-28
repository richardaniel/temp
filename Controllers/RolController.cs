
using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Roles;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Roles.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class RolController : ApiController
    { 
        private readonly RolService _rolService;

        public RolController(RolService rolService)
        {
            _rolService = rolService;
        }

        [HttpPost("crearRol")]
        public async Task<IActionResult> crearRol([FromBody] RolDto rolDto)
        {
            var crearRolResult = await _rolService.CrearRol(rolDto);
            return crearRolResult.Match(
                rolCreado => Ok(crearRolResult.Value),
                errors => Problem(errors));
        }
    }
}
