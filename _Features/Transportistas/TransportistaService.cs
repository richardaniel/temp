using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Transportistas.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Transportistas
{
    public class TransportistaService
    {

        private readonly IRepository<Transportista> _transportistaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransportistaService(IRepository<Transportista> transportistaRepository, IUnitOfWork unitOfWork)
        {
            _transportistaRepository = _unitOfWork.Repository<Transportista>();
            _unitOfWork = unitOfWork;
        }

        public async Task <ErrorOr<string>> RegistrarTransportista(TransportistaDto request)
        {
            try
            {
                var transportista = new Transportista
                {
                    TransportistaId = 0,
                    Nombre = request.Nombre,
                    Email = request.Email,
                    PaisId = request.PaisId,
                    EstadoId = request.EstadoId,
                    CiudadId = request.CiudadId,
                    Activo = true,
                    Fechacreacion = DateTime.Now,
                    Fechaactualizacion = DateTime.Now
                };
                await _transportistaRepository.AddAsync(transportista);
                await _unitOfWork.SaveChangesAsync();
                return "Transportista registrado correctamente";
            }
            catch (Exception ex)
            {
                return Error.Failure("Error al registrar el transportista", ex.Message);
            }
        }

    }
}
