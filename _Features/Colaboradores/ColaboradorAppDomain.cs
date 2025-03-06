using ErrorOr;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales.Dtos;
using System.Text.RegularExpressions;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores
{

 

    public class ColaboradorAppDomain
    {
        private readonly ValidateEmailDomain _validateEmailDomain;
        private readonly string errorCodeValidation = "General.Validation";
        private readonly string errorCodeConflict = "General.Conflict";

        public ColaboradorAppDomain(ValidateEmailDomain validateEmailDomain)
        {
            _validateEmailDomain = validateEmailDomain;
        }

        public ErrorOr<Colaborador> ValidateColaboradorForCreation(Colaborador colaborador , ColaboradorDomainRequeriment colaboradorDomainRequeriment)
        {
            var emailValidated = _validateEmailDomain.ValidateEmail(colaborador.Email);
            

            if (emailValidated.IsError)
            {
                return Error.Validation(errorCodeValidation, emailValidated.FirstError.Description);
            }

            if(colaboradorDomainRequeriment.IsCorreoExiste)
            {
                return Error.Conflict(errorCodeConflict, MensajesGlobales.EmailYaExiste);
            }

            if (string.IsNullOrEmpty(colaborador.Nombre))
            {
               return Error.Validation(errorCodeValidation, MensajesGlobales.NombreRequerido);
            }

            if(string.IsNullOrEmpty(colaborador.Apellido))
            {
                return Error.Validation(errorCodeValidation, MensajesGlobales.ApellidoRequerido);
            }

            if(string.IsNullOrEmpty(colaborador.Telefono))
            {
                return Error.Validation(errorCodeValidation, MensajesGlobales.TelefonoRequerido);
            }

            if (colaborador.Latitud.Equals(0) && colaborador.Longitud.Equals(0))
            {
               return Error.Validation(errorCodeValidation, MensajesGlobales.CoordenadasInvalidas);
            }

            if (colaborador.Latitud < -90 || colaborador.Latitud > 90)
                return Error.Validation(errorCodeValidation,MensajesGlobales.LatitudInvalida );


            if (colaborador.Longitud < -180 || colaborador.Longitud > 180)
                return Error.Validation(errorCodeValidation, MensajesGlobales.LongitudInvalida);

           


            if(!colaboradorDomainRequeriment.IsPaisExiste)
            {
                return Error.Conflict(errorCodeConflict, MensajesGlobales.ObjetoNoExiste.Replace("@Objeto", "pais"));

            }
            if (!colaboradorDomainRequeriment.IsEstadoExiste)
                return Error.Conflict(errorCodeConflict, MensajesGlobales.ObjetoNoExiste.Replace("@Objeto", "estado"));

            if(!colaboradorDomainRequeriment.IsCiudadExiste)
            {
                return Error.Conflict(errorCodeConflict, MensajesGlobales.ObjetoNoExiste.Replace("@Objeto", "ciudad"));
            }
            colaborador.Email = emailValidated.Value;
            colaborador.Fechaactualizacion = DateTime.Now;
            colaborador.Fechacreacion = DateTime.Now;
            colaborador.Activo = true;

            return colaborador;
            }


        public ErrorOr<string> ValidateColaboradorForUpdate(EditarColaboradorDto colaborador)
        {
            var emailValidation = _validateEmailDomain.ValidateEmail(colaborador.Email);
            if (emailValidation.IsError)
            {
                return Error.Validation("General.Validation", MensajesGlobales.EmailInvalido);
            }
            return String.Format(MensajesGlobales.EntidadActualizadaCorrectamente,"colaborador");
        }            
    }
}

