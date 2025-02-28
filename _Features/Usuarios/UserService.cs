

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ErrorOr;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios.HashPassword;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios.Dtos;

using Richar.Academia.ProyectoFinal.WebAPI._Features.Roles;



namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios
{
    public class UserService
    {
        private readonly SistemaTransporteContext _context;
        private readonly IConfiguration _configuration;
       // private readonly IHashPassword _hashPassword;

        public UserService(SistemaTransporteContext context, IConfiguration configuration, IHashPassword hashPassword)
        {
            _context = context;
            _configuration = configuration;
          //  _hashPassword = hashPassword;
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public async Task<ErrorOr<string>> Autenticacion(LoginRequest request)
        {

            Colaborador? colaborador = _context.Colaboradores.FirstOrDefault(x => x.Email == request.Email);



            if (colaborador == null)
            {
                return Error.NotFound("Usuario.NoEncontrado", $"El usuario con el correo {request.Email} no se encontro.");
            }

            Usuario? usuario = _context.Usuarios.FirstOrDefault(x => x.ColaboradorId == colaborador.ColaboradorId);

            if (usuario == null)
            {
                return Error.Failure("General.Failure", "Invalid email or password");
            }
            string bcryptHash = System.Text.Encoding.UTF8.GetString(usuario.Contrasena);
           
            if (!BCrypt.Net.BCrypt.Verify(request.Password, bcryptHash))
            {
                return Error.Failure("General.Failure","Invalid email or password");
            }


            Rol? rol = _context.Roles.FirstOrDefault(x => x.RolId == usuario.RolId);

            return GenerateJwtToken(colaborador.Nombre, colaborador.Email, rol.Nombre);

        }

      

        public string GenerateJwtToken(string name, string email, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name , name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<ErrorOr<string>> Registro (UsuarioDto usuario)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
            byte[] passwordHashBytes = Encoding.UTF8.GetBytes(passwordHash);


            Usuario _usuario = new Usuario
            {
                Nombre = usuario.Nombre,
                Contrasena = passwordHashBytes,
                RolId = usuario.RolId,
                ColaboradorId = usuario.ColaboradorId
            };
            _context.Usuarios.Add(_usuario);
            _context.SaveChanges();

            return "Usuario registrado correctamente";
        }
    }
}
