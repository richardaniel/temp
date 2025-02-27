using ErrorOr;
using System.Text.RegularExpressions;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common
{
    public class ValidateEmailDomain
    {

        private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public ErrorOr<bool> ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Error.Failure("General.Failure", "El correo electrónico no puede estar vacío.");
            }

            if (!Regex.IsMatch(email, EmailPattern))
            {
                return Error.Failure("General.Failure", "El formato del correo electrónico es incorrecto.");
            }

            return true;
        }
    }
}
