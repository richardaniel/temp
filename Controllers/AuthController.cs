
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.Data; 
using Microsoft.IdentityModel.Tokens;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios.HashPassword;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly UserService _userService;

        

        public AuthController(UserService userService)
        {

            _userService = userService;

        }


        [HttpPost("login")]


        public async Task<IActionResult> Login([FromBody] LoginRequest reqest)
        {
            var result = await _userService.Autenticacion(reqest);
            return result.Match(
                resultadoOk => Ok(result.Value),
                errors => Problem(errors));
            
        }


        [HttpPost("registro")]
        public async Task<IActionResult> Register([FromBody] UsuarioDto request)
        {

            var result = await _userService.Registro(request);
            return Ok(result.Value);
        }
    }
}
