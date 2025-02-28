using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Transportistas.Dtos;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Transportistas
{
    public class TransportistaService
    {
        private readonly IRepository<Transportista> _transportistaRepository;
        private readonly IRepository<Pais> _paises;
        private readonly IRepository<Estado> _estados;
        private readonly IRepository<Ciudad> _ciudades;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TransportistaAppDomain _transportistaDomain;

        public TransportistaService(IUnitOfWork unitOfWork , TransportistaAppDomain transportistaAppDomain)
        {
            _unitOfWork = unitOfWork;
            _transportistaRepository = unitOfWork.Repository<Transportista>();
            _paises = unitOfWork.Repository<Pais>();
            _estados = unitOfWork.Repository<Estado>();
            _ciudades = unitOfWork.Repository<Ciudad>();
            _transportistaDomain = transportistaAppDomain;
        }

        public async Task<ErrorOr<string>> RegistrarTransportista(TransportistaDto request)
        {
            try
            {
               
                var domainValidation = _transportistaDomain.ValidateTransportista(request);
                if (domainValidation.IsError)
                    return domainValidation.Errors;

               
                var duplicateExists = _transportistaRepository.AsQueryable()
                    .Any(t => t.Nombre == request.Nombre || t.Email == request.Email);
                if (duplicateExists)
                    return Error.Conflict("Ya existe un transportista con el mismo nombre o email.");

                if (!_paises.AsQueryable().Any(p => p.PaisId == request.PaisId))
                    return Error.NotFound($"No se encontró un país con el ID {request.PaisId}.");
                if (!_estados.AsQueryable().Any(e => e.EstadoId == request.EstadoId))
                    return Error.NotFound($"No se encontró un estado con el ID {request.EstadoId}.");
                if (!_ciudades.AsQueryable().Any(c => c.CiudadId == request.CiudadId))
                    return Error.NotFound($"No se encontró una ciudad con el ID {request.CiudadId}.");

                bool correoExiste = _transportistaRepository.AsQueryable().Where(t => t.Email.Equals(request.Email)).Any();

                if (correoExiste)
                {
                    return Error.Failure("Correo.YaExiste", "El correo ya esta asociado a un Transportista");
                }
                var transportista = new Transportista
                {
                    TransportistaId = 0, 
                    Nombre = request.Nombre,
                    Email = request.Email,
                    PaisId = request.PaisId,
                    EstadoId = request.EstadoId,
                    CiudadId = request.CiudadId,
                    Activo = true,
                    Fechacreacion = DateTime.Now,
                    Fechaactualizacion = DateTime.Now
                };

                await _transportistaRepository.AddAsync(transportista);
                await _unitOfWork.SaveChangesAsync();
                return "Transportista registrado correctamente";
            }
            catch (Exception ex)
            {
                return Error.Failure("Error al registrar el transportista", ex.Message);
            }
        }
    }
}