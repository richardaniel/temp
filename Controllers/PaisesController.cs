using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;

namespace Richar.Academia.ProyectoFinal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Pais> _paisesRepository;

        public PaisesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _paisesRepository = unitOfWork.Repository<Pais>();
        }

        [HttpGet]
        public IActionResult ObtenerPaises()
        {
            var paisesResult = _paisesRepository.AsQueryable().ToList();

            return Ok(paisesResult);
        }
    }
}
