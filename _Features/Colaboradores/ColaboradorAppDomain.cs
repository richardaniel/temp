using ErrorOr;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos;
using System.Text.RegularExpressions;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores
{

 

    public class ColaboradorAppDomain
    {
        private readonly ValidateEmailDomain _validateEmailDomain;
        public ColaboradorAppDomain(ValidateEmailDomain validateEmailDomain)
        {
            _validateEmailDomain = validateEmailDomain;
        }

        public ErrorOr<bool> ValidateColaboradorForCreation(ColaboradorDto colaborador)
        {
            var emailValidation = _validateEmailDomain.ValidateEmail(colaborador.Email);
            if (emailValidation.IsError)
            {
               return emailValidation;
            }

            if (string.IsNullOrEmpty(colaborador.Nombre))
            {
               return Error.Failure("General.Failure", "El nombre del colaborador es obligatorio.");
            }

            if (colaborador.Latitud.Equals(0) && colaborador.Longitud.Equals(0))
            {
               return Error.Failure("General.Failure", "La latitud o Longitud no puede ser cero");
            }

            if(colaborador.Pais.PaisId <= 0)
            {
                return Error.Failure("General.Failure", "El pais no puede estar vacio");
            }

            if (colaborador.Estado.EstadoId <= 0)
            {
                return Error.Failure("General.Failure", "El estado no puede estar vacio");
            }

            if (colaborador.Ciudad.CiudadId <= 0)
            {
                return Error.Failure("General.Failure", "La ciudad no puede estar vacia ");
            }

            return true;
            }
            

        public ErrorOr<bool> ValidateColaboradorForUpdate(EditarColaboradorDto colaborador)
            {
                var emailValidation = _validateEmailDomain.ValidateEmail(colaborador.Email);
                if (emailValidation.IsError)
                {
                    return emailValidation;
                }

               
                return true;
            }
    
    
        

    }
}

