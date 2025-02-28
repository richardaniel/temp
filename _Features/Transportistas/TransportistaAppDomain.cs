using ErrorOr;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Transportistas.Dtos;
using System.Text.RegularExpressions;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Transportistas
{
    public class TransportistaAppDomain
    {

        private readonly ValidateEmailDomain _validateEmailDomain;

        public TransportistaAppDomain(ValidateEmailDomain validateEmailDomain)
        {
            _validateEmailDomain = validateEmailDomain;
        }

        public ErrorOr<bool> ValidateTransportista(TransportistaDto transportistaDto)
        {
            if (string.IsNullOrWhiteSpace(transportistaDto.Nombre))
                return Error.Validation("El nombre del transportista es obligatorio.");
            if (string.IsNullOrWhiteSpace(transportistaDto.Email))
                return Error.Validation("El email del transportista es obligatorio.");
            if (transportistaDto.PaisId <= 0)
                return Error.Validation("El ID del país es obligatorio y debe ser válido.");
            if (transportistaDto.EstadoId <= 0)
                return Error.Validation("El ID del estado es obligatorio y debe ser válido.");
            if (transportistaDto.CiudadId <= 0)
                return Error.Validation("El ID de la ciudad es obligatorio y debe ser válido.");

           
            if (transportistaDto.Nombre.Length > 100)
                return Error.Validation("El nombre no puede exceder los 100 caracteres.");
            if (transportistaDto.Email.Length > 150)
                return Error.Validation("El email no puede exceder los 150 caracteres.");

            if (_validateEmailDomain.ValidateEmail(transportistaDto.Email).IsError)
                return Error.Validation("El email tiene un formato inválido.");

            return true; 
        }

        
    }
}