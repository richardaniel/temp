using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Estados.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales
{
    public class SucursalService
    {
       private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Sucursal> _sucursales;

        public SucursalService(IUnitOfWork unitOfWork)
        {
            _sucursales = unitOfWork.Repository<Sucursal>();
        }

        public async Task<ErrorOr<string>> RegistrarSucursal(SucursalDto Sucursal)
        {
            
            try
            {

                var sucursal = new Sucursal
                {
                    SucursalId = Sucursal.sucursalId,
                    Nombre =Sucursal.Nombre,
                    Direccion = Sucursal.Direccion,
                    Latitud = Sucursal.Latitud,
                    Longitud = Sucursal.Longitud,
                    PaisId = Sucursal.Pais.PaisId,
                    EstadoId = Sucursal.Estado.EstadoId,
                    CiudadId = Sucursal.Ciudad.CiudadId,
                   
                };
                sucursal.Activo = true;
                sucursal.Fechacreacion = DateTime.Now;
                sucursal.Fechaactualizacion = DateTime.Now;
                await _sucursales.AddAsync(sucursal);
                await _unitOfWork.SaveChangesAsync();


                return "Sucursal registrada exitosamente";
            }
            catch (Exception e)
            {
                return Error.Failure(e.Message);
            }

        }

        public async Task<ErrorOr<List<SucursalDto>>> ObtenerSucursales()
        {
            try
            {
                var sucursales = _sucursales
                    .AsQueryable()
                    .Include(x=>x.Pais)
                    .Include(x=>x.Estado)
                    .Include(x=>x.Ciudad)
                    .Select(x => new SucursalDto
                    {
                        sucursalId = x.SucursalId,
                        Nombre = x.Nombre,
                        Latitud = x.Latitud,
                        Longitud = x.Longitud,
                        Direccion = x.Direccion,
                        Pais = new PaisDto
                        {
                            PaisId = x.Pais.PaisId,
                            Nombre = x.Pais.Nombre
                        },
                        Estado = new EstadoDto
                        {
                            EstadoId = x.Estado.EstadoId,
                            Nombre = x.Estado.Nombre
                        },
                        Ciudad = new CiudadDto
                        {
                            CiudadId = x.Ciudad.CiudadId,
                            Nombre = x.Ciudad.Nombre
                        },

                    })
                    .ToList();

                return sucursales;
            }
            catch (Exception ex)
            {
                return Error.Failure("Error al obtener sucursales", ex.Message);
            }
        }
    }
}
