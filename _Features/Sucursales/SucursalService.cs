using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Estados.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales.Dtos;


namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales
{
    public class SucursalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Sucursal> _sucursales;
        private readonly IRepository<Pais> _paises;
        private readonly IRepository<Estado> _estados;
        private readonly IRepository<Ciudad> _ciudades;
        private readonly SucursalAppDomain _sucursalDomain;

        public SucursalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sucursales = unitOfWork.Repository<Sucursal>();
            _paises = unitOfWork.Repository<Pais>();
            _estados = unitOfWork.Repository<Estado>();
            _ciudades = unitOfWork.Repository<Ciudad>();
            _sucursalDomain = new SucursalAppDomain();
        }

        public async Task<ErrorOr<string>> RegistrarSucursal(SucursalDto sucursalDto)
        {
            try
            {
                var domainValidation = _sucursalDomain.ValidateSucursal(sucursalDto);
                if (domainValidation.IsError)
                    return domainValidation.Errors;

              
                var duplicateExists = _sucursales.AsQueryable()
                    .Any(s => s.Nombre == sucursalDto.Nombre ||
                             (s.Latitud == sucursalDto.Latitud && s.Longitud == sucursalDto.Longitud));
                if (duplicateExists)
                    return Error.Conflict("Ya existe una sucursal con el mismo nombre o ubicación.");

             
                if (!_paises.AsQueryable().Any(p => p.PaisId == sucursalDto.Pais.PaisId))
                    return Error.NotFound($"No se encontró un país con el ID {sucursalDto.Pais.PaisId}.");
                if (!_estados.AsQueryable().Any(e => e.EstadoId == sucursalDto.Estado.EstadoId))
                    return Error.NotFound($"No se encontró un estado con el ID {sucursalDto.Estado.EstadoId}.");
                if (!_ciudades.AsQueryable().Any(c => c.CiudadId == sucursalDto.Ciudad.CiudadId))
                    return Error.NotFound($"No se encontró una ciudad con el ID {sucursalDto.Ciudad.CiudadId}.");


         

                var sucursal = new Sucursal
                {

                     
                SucursalId = sucursalDto.sucursalId,
                    Nombre = sucursalDto.Nombre,
                    Direccion = sucursalDto.Direccion,
                    Latitud = sucursalDto.Latitud,
                    Longitud = sucursalDto.Longitud,
                    PaisId = sucursalDto.Pais.PaisId,
                    EstadoId = sucursalDto.Estado.EstadoId,
                    CiudadId = sucursalDto.Ciudad.CiudadId,
                    Activo = true,
                    Fechacreacion = DateTime.Now,
                    Fechaactualizacion = DateTime.Now
                };

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
                    .Include(x => x.Pais)
                    .Include(x => x.Estado)
                    .Include(x => x.Ciudad)
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
                           
                        },
                        Estado = new EstadoDto
                        {
                            EstadoId = x.Estado.EstadoId,
                           
                        },
                        Ciudad = new CiudadDto
                        {
                            CiudadId = x.Ciudad.CiudadId,
                           
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