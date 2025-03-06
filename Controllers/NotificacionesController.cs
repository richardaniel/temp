using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Notificaciones;
using System.Runtime.CompilerServices;

namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ApiController
    {
        private readonly NotificacionService _notificacionService;

        public NotificacionesController(NotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        [HttpPost("enviarNotificacion")]
        public async Task<IActionResult> EnviarNotificacion()
        {
            var enviarNotificacionResult = await _notificacionService.SendNotificationAllAsync("¡Hola!", "Esto es una notificacion de prueba");
            return enviarNotificacionResult.Match(
                notificacionEnviada => Ok(enviarNotificacionResult.Value),
                errors => Problem(errors));
        }
    }
}
