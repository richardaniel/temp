using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Dispositivos;
using FirebaseAdmin.Messaging;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Notificaciones
{
    public class NotificacionService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Dispositivo> _dispossitivosRepository;
        public NotificacionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            _dispossitivosRepository = unitOfWork.Repository<Dispositivo>();
        }

        public async Task<ErrorOr<bool>> SendNotificationAllAsync(string title , string body )
        {
            var devices = _dispossitivosRepository.AsQueryable().ToList();

            var tokens = devices.Select(x => x.Token).ToList();
            
            if(!tokens.Any())
            {
                return Error.Failure("General.Failure", "No se encontro ningun dispositivo para enviar notificacion");
            }
            var message = new MulticastMessage()
            {
                Tokens = tokens,
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                }
            };

            await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message);

            return true;
        }

        public async Task<ErrorOr<bool>> SendNotificationAsync(string title, string body, int userId)
        {

            var dispositivo = _dispossitivosRepository.AsQueryable().FirstOrDefault(x => x.UsuarioId == userId);

            if (dispositivo == null)
            {
                return Error.Failure("General.Failure", "No se encontro ningun dispositivo para enviar notificacion");
            }

            var token = dispositivo.Token;

            var message = new Message()
            {
                Token = token,
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                }
            };
            await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return true;
        }
    }
}
