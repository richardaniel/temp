namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Helpers
{
    public interface IGenericValidatorService
    {
        Task<bool> ExistsAsync<TEntity>(int id) where TEntity : class;

        Task<bool> IsEmailAsociado<Tentity>(string email) where Tentity : class;
    }
}
