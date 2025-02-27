using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;


namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios.HashPassword
{
    public class HashPasswordService:IHashPassword
    {


        public string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password must not be empty or null.", nameof(password));
            }

            // Generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive a 256-bit subkey (use HMACSHA256 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Combine salt and hash for storage
            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        public bool Verify(string password, string hash)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (hash == null) throw new ArgumentNullException(nameof(hash));

            var parts = hash.Split('.', 2);
            if (parts.Length != 2) throw new FormatException("Unexpected hash format");

            var salt = Convert.FromBase64String(parts[0]);
            var hashToCompare = parts[1];

            string hashedInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashToCompare == hashedInput;
        }

      
    }
}
