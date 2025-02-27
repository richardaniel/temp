namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios.Dtos
{
    public class UsuarioDto
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int ColaboradorId { get; set; }
        public int RolId { get; set; }
        public bool Activo { get; set; }
    }
}
