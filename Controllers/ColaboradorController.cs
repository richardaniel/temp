
using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos;


namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
  
    public class ColaboradorController : ApiController
    {

        private readonly ColaboradorService _colaboradorService;

        public ColaboradorController(ColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }
        #region POST
        [HttpPost("CrearColaborador")]
        public async Task<IActionResult> CrearColaborador([FromBody] ColaboradorDtoRequest colaborador)
        {
            var createColaboradorResult =await _colaboradorService.CrearColaborador(colaborador);

            return createColaboradorResult.Match(
               colaboradorCreado =>Ok(createColaboradorResult.Value),
               errors => Problem(errors) 
           );
        }

        #endregion

        #region GET
        [HttpGet("ObtenerColaborador/{id}")]
        public IActionResult ObtenerColaboradorById(int id)
        {
            return Ok();
        }


        [HttpGet("ObtenerColaboradores")]
        public IActionResult ObtenerColaboradores()
        {
            List <ColaboradorDtoResponse> colaboradores = _colaboradorService.ObtenerColaboradores();
            return Ok(colaboradores);
        }
        #endregion

        #region DELETE  
        [HttpDelete("EliminarColaborador")]
        public async Task<IActionResult> EliminarColaborador(int id)
        {
            var eliminarColaboradorResult = await _colaboradorService.EliminarColaborador(id);

            return eliminarColaboradorResult.Match(
                colaboradorEliminado =>Ok(),
                errors => Problem(errors)
                );
        }
        #endregion

        #region PATCH
        [HttpPatch("DesactivarColaborador/{id}")]
        public async Task<IActionResult> DesactivarColaborador(int id)
        {
            var desactivarColaboradorResult = await _colaboradorService.DesactivarColaborador(id);
            return desactivarColaboradorResult.Match(
                    colaboradorDesactivado => Ok(desactivarColaboradorResult),
                    errors => Problem(errors)
                );

        }
        #endregion 

        #region PUT
        [HttpPut("EditarColaborador/{id}")]
        public async Task<IActionResult> EditarColaborador(int id , [FromBody] EditarColaboradorDto request)
        {
            var editarColaboradorResullt = await _colaboradorService.EditarColaborador(id, request);

            return editarColaboradorResullt.Match(
                    colaboradorEditado => Ok(editarColaboradorResullt),
                    errors => Problem(errors));
        }
        #endregion 
    }
}
