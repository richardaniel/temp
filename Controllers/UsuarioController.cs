using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios;

namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UserService _userService;

        public UsuarioController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("ObtenerUsuarios")]
        public IActionResult Get()
        {
            var listado =_userService.ObtenerUsuarios();
            return Ok(listado);
        }
    }
}
