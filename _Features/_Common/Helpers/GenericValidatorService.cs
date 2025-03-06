using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte;
using System.Threading.Tasks;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Helpers
{
    public class GenericValidatorService : IGenericValidatorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SistemaTransporteContext _context;

        public GenericValidatorService(IUnitOfWork unitOfWork,SistemaTransporteContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<bool> IsEmailAsociado<TEntity>(string email) where TEntity : class
        {
            var repository = _unitOfWork.Repository<TEntity>();

            bool IsAsociado = await repository.AsQueryable().AnyAsync(e => EF.Property<string>(e,"Email")==email);

            return IsAsociado;
        }

        public async Task<bool> ExistsAsync<TEntity>(int id) where TEntity : class
        {
            var repository = _unitOfWork.Repository<TEntity>();

            
            var keyName = _context.GetPrimaryKeyName<TEntity>();

            var entity = await repository.FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
            return entity != null;
        }

    }
}
