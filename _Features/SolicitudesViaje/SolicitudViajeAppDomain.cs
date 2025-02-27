using ErrorOr;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje
{
    public class SolicitudViajeAppDomain
    {


        public ErrorOr<bool> ValidateSolicitudViajeDto(SolicitudViajeDto request)
        {
            if (request == null)
                return Error.Failure("Los datos de la solicitud de viaje no pueden ser nulos");

            if (request.ColaboradorId <= 0)
                return Error.Failure("El ID del colaborador debe ser un valor válido mayor a cero");

            if (request.SucursalId <= 0)
                return Error.Failure("El ID de la sucursal debe ser un valor válido mayor a cero");

            if (request.EstadoSolicitudId <= 0)
                return Error.Failure("El ID del estado de la solicitud debe ser un valor válido mayor a cero");

            if (string.IsNullOrWhiteSpace(request.Comentario))
                return Error.Failure("El comentario de la solicitud no puede estar vacío");

            if (request.FechaSolicitud == default(DateTime))
                return Error.Failure("La fecha de la solicitud debe ser una fecha válida");

            if (request.FechaSolicitud < DateTime.Today)
                return Error.Failure("La fecha de la solicitud no puede ser anterior a la fecha actual");

            return true;
        }
    }
}


