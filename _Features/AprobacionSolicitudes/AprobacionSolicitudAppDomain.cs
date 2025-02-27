using ErrorOr;
using Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes
{
    public class AprobacionSolicitudAppDomain
    {
       
            public ErrorOr<bool> ValidateSingleRequest(AprobacionSolicitudDto request)
            {
                if (request == null)
                    return Error.Failure("Los datos de la solicitud no pueden ser nulos");

                if (request.SolicitudId <= 0)
                    return Error.Failure("El ID de la solicitud debe ser un valor válido mayor a cero");

                if (request.EstadoSolicitudId <= 0)
                    return Error.Failure("El ID del estado de la solicitud debe ser un valor válido mayor a cero");

                if (request.GerenteId <= 0)
                    return Error.Failure("El ID del gerente debe ser un valor válido mayor a cero");

                return true;
            }

            public ErrorOr<bool> ValidateMultipleRequests(List<AprobacionSolicitudDto> requests)
            {
                if (requests == null || !requests.Any())
                    return Error.Failure("La lista de solicitudes no puede ser nula o estar vacía");

                foreach (var request in requests)
                {
                    var validation = ValidateSingleRequest(request);
                    if (validation.IsError)
                        return validation; // Retorna el primer error encontrado

                    // Validación específica para cada request en la lista
                    if (request.SolicitudId <= 0)
                        return Error.Failure("Uno de los IDs de solicitud no es válido");

                    if (request.EstadoSolicitudId <= 0)
                        return Error.Failure("Uno de los estados de solicitud no es válido");

                    if (request.GerenteId <= 0)
                        return Error.Failure("Uno de los IDs de gerente no es válido");
                }

                return true;
            }

            public ErrorOr<bool> ValidateSolicitudViaje(SolicitudViaje solicitudViaje, int requestSolicitudId, int estadoSolicitudId)
            {
                if (solicitudViaje == null)
                    return Error.Failure($"Solicitud de viaje con ID {requestSolicitudId} no encontrada en el sistema");

                if (solicitudViaje.EstadoSolicitudId.Equals(EstadoSolicitudes.Dtos.EstadoSolicitudEnum.EstadoSolicitud.Rechazada))
                    return Error.Failure($"La solicitud con ID {requestSolicitudId} ya ha sido rechazada previamente");

                if (solicitudViaje.EstadoSolicitudId.Equals(EstadoSolicitudes.Dtos.EstadoSolicitudEnum.EstadoSolicitud.Aprobada))
                    return Error.Failure($"La solicitud con ID {requestSolicitudId} ya ha sido aprobada previamente");

                if (estadoSolicitudId == 3) // Aprobada
                {
                    if (solicitudViaje.SucursalId <= 0)
                        return Error.Failure($"La solicitud con ID {requestSolicitudId} no tiene una sucursal válida asociada");

                    if (solicitudViaje.ColaboradorId <= 0)
                        return Error.Failure($"La solicitud con ID {requestSolicitudId} no tiene un colaborador válido asociado");
                }
                else if (estadoSolicitudId != 2) // Rechazada
                {
                    return Error.Failure($"Estado no válido para la solicitud con ID {requestSolicitudId}");
                }

                return true;
            }

            public ErrorOr<bool> ValidateViajeCreation(int monedaId, int viajeId, int solicitudId)
            {
                if (monedaId <= 0)
                    return Error.Failure("No se pudo obtener una moneda válida para el viaje");

                if (viajeId <= 0)
                    return Error.Failure($"No se pudo crear el registro del nuevo viaje para la solicitud con ID {solicitudId}");

                return true;
            }

            public ErrorOr<bool> ValidateColaboradorSucursal(ColaboradorSucursal? colaboradorSucursal, int solicitudId)
            {
                if (colaboradorSucursal == null)
                    return Error.Failure($"No se encontró el registro del colaborador en la sucursal para la solicitud con ID {solicitudId}");

                return true;
            }
        }
    
}
