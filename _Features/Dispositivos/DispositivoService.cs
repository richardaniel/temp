using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.Identity.Client;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Dispositivos
{

    

    public class DispositivoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Dispositivo> _dispositivoRepository;

        public DispositivoService(IUnitOfWork unitOfWork)
        {
           
           
            _unitOfWork = unitOfWork;
            _dispositivoRepository = unitOfWork.Repository<Dispositivo>();
        }

        public async Task<ErrorOr<bool>> RegistrarDispositivo(string token)
        {
            if(string.IsNullOrEmpty(token))
            {
                return Error.Failure("General.Failure", "EL token es requerdio");
            }

            var device = new Dispositivo { Token = token };

            await _dispositivoRepository.AddAsync(device);
            await _unitOfWork.SaveChangesAsync();




            return true;
        }
    }
}
