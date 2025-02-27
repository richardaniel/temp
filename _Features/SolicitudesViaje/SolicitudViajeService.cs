// Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje/SolicitudViajeService.cs
using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte;

using System.Data.Common;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje
{
    public class SolicitudViajeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SolicitudViaje> _solicitudViajeRepository;
        private readonly SolicitudViajeAppDomain _validatorDomain;

        public SolicitudViajeService(IUnitOfWork unitOfWork,
            SistemaTransporteContext context,
            SolicitudViajeAppDomain validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "La unidad de trabajo no puede ser nula");
            _solicitudViajeRepository = unitOfWork.Repository<SolicitudViaje>();
            _validatorDomain = validator ?? throw new ArgumentNullException(nameof(validator), "El validador no puede ser nulo");
        }

        public async Task<ErrorOr<string>> RegistrarSolicitudViaje(SolicitudViajeDto request)
        {
            var validationResult = _validatorDomain.ValidateSolicitudViajeDto(request);
            if (validationResult.IsError)
                return validationResult.FirstError;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var solicitudViaje = new SolicitudViaje
                {
                    ColaboradorId = request.ColaboradorId,
                    SucursalId = request.SucursalId,
                    EstadoSolicitudId = request.EstadoSolicitudId,
                    Comentario = request.Comentario,
                    FechaSolicitud = request.FechaSolicitud
                };

                await _solicitudViajeRepository.AddAsync(solicitudViaje);
                await _unitOfWork.SaveChangesAsync();

                if (solicitudViaje.SolicitudViajeId <= 0)
                    return Error.Failure("No se pudo generar un ID válido para la nueva solicitud de viaje");

                await _unitOfWork.CommitAsync();
                return "Solicitud de viaje registrada correctamente";
            }
            catch (DbException ex)
            {
                await _unitOfWork.RollBackAsync();
                return Error.Failure("Error en la base de datos al registrar la solicitud de viaje", ex.Message);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackAsync();
                return Error.Failure("Error inesperado al registrar la solicitud de viaje", ex.Message);
            }
        }
    }
}