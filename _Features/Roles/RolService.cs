using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Roles.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Roles
{
    public class RolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Rol> _rolrepository;

        public RolService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _rolrepository = unitOfWork.Repository<Rol>();
        }

        public async Task<ErrorOr<string>> CrearRol(RolDto rolDto)
        {
            var rol = new Rol
            {
                RolId = 0,
                Nombre = rolDto.Nombre,
                Descripcion = rolDto.Descripcion,
                Fechaactualizacion = DateTime.Now,
                Fechacreacion = DateTime.Now
            };

            await _rolrepository.AddAsync(rol);
            await _unitOfWork.SaveChangesAsync();


            return "Rol creado correctamente";

        }
    }
}
