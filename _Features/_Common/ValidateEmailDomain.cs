using ErrorOr;
using System.Text.RegularExpressions;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common
{
    public class ValidateEmailDomain
    {

        private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public ErrorOr<string> ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Error.Validation("General.Validation", MensajesGlobales.EmailRequerido);
            }

            if (!Regex.IsMatch(email, EmailPattern))
            {
                return Error.Validation("General.Validation", MensajesGlobales.EmailInvalido);
            }

            return email;
        }
    }
}
