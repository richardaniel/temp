using ErrorOr;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales
{
    public class SucursalAppDomain
    {
        public ErrorOr<bool> ValidateSucursal(SucursalDto sucursalDto)
        {
          
            if (string.IsNullOrWhiteSpace(sucursalDto.Nombre))
                return Error.Validation("General.Validation","El nombre de la sucursal es obligatorio.");

            if (string.IsNullOrWhiteSpace(sucursalDto.Direccion))
                return Error.Validation("General.Validation","La dirección de la sucursal es obligatoria.");

            if (sucursalDto.Pais == null || sucursalDto.Pais.PaisId <= 0)
                return Error.Validation("General.Validation", "El ID del país es obligatorio y debe ser válido.");

            if (sucursalDto.Estado == null || sucursalDto.Estado.EstadoId <= 0)
                return Error.Validation("General.Validation", "El ID del estado es obligatorio y debe ser válido.");

            if (sucursalDto.Ciudad == null || sucursalDto.Ciudad.CiudadId <= 0)
                return Error.Validation("General.Validation", "El ID de la ciudad es obligatorio y debe ser válido.");

            if (sucursalDto.Nombre.Length > 100)
                return Error.Validation("General.Validation", "El nombre no puede exceder los 100 caracteres.");

            if (sucursalDto.Direccion.Length > 200)
                return Error.Validation("General.Validation", "La dirección no puede exceder los 200 caracteres.");

            if (sucursalDto.Latitud ==0 || sucursalDto.Latitud == 0)
                return Error.Validation("General.Validation", "La latitud no puede ser 0.");

            if (sucursalDto.Latitud < -90 || sucursalDto.Latitud > 90)
                return Error.Validation("General.Validation", "La latitud debe estar entre -90 y 90.");

            if (sucursalDto.Longitud < -180 || sucursalDto.Longitud > 180)
                return Error.Validation("General.Validation", "La longitud debe estar entre -180 y 180.");

            return true; // Validation passed
        }
    }
}