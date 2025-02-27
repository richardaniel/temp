namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios.HashPassword
{
    public interface IHashPassword
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }
}
