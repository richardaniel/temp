using ErrorOr;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes.Dtos;




namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes
{
    public class ViajeAppDomain
    {
        private const decimal MaxDistanciaKm = 100m;

        public ErrorOr<bool> ValidateCrearViaje(ViajeDto viajeDto)
        {
            if (viajeDto.SucursalId <= 0)
                return Error.Validation("El ID de la sucursal es obligatorio y debe ser válido.");
            if (viajeDto.TransportistaId <= 0)
                return Error.Validation("El ID del transportista es obligatorio y debe ser válido.");
            if (viajeDto.UsuarioRegistroId <= 0)
                return Error.Validation("El ID del usuario registro es obligatorio y debe ser válido.");
            if (viajeDto.DistanciaTotalKm < 0)
                return Error.Validation("La distancia total no puede ser negativa.");
            if (viajeDto.CostoTotal < 0)
                return Error.Validation("El costo total no puede ser negativo.");
            return true;
        }

        public ErrorOr<bool> ValidateViajeDistance(decimal distanciaTotalKm)
        {
            if (distanciaTotalKm < 0)
                return Error.Validation("La distancia total no puede ser negativa.");
            if (distanciaTotalKm > MaxDistanciaKm)
                return Error.Validation($"Un solo viaje no puede superar los {MaxDistanciaKm} km; se debe dividir.");
            return true;
        }

        public ErrorOr<bool> ValidateActualizarViajes(DateTime fechaViaje)
        {
            if (fechaViaje > DateTime.Now)
                return Error.Validation("La fecha del viaje no puede ser futura.");
            return true;
        }

        public ErrorOr<bool> ValidateReporteTransportista(DateTime fechaInicio, DateTime fechaFin, int transportistaId)
        {
            if (fechaInicio > fechaFin)
                return Error.Validation("La fecha de inicio no puede ser mayor que la fecha de fin.");
            if (fechaFin > DateTime.Now)
                return Error.Validation("La fecha de fin no puede ser futura.");
            if (transportistaId <= 0)
                return Error.Validation("El ID del transportista debe ser válido.");
            return true;
        }
    }
}