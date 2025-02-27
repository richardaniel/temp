using AutoMapper;
using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Estados.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises.Dtos;
namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores
{
    public class ColaboradorService
    {
        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ColaboradorAppDomain _validatorDomain;
        private readonly IRepository<Colaborador> _colaboradorRepository;
        private readonly ValidateEmailDomain _validatorEmailDomain;

        public ColaboradorService(IMapper mapper,ColaboradorAppDomain validator ,ValidateEmailDomain validateEmailDomain,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validatorDomain = validator;
            _validatorEmailDomain = validateEmailDomain;
            _colaboradorRepository = unitOfWork.Repository<Colaborador>();
        }

        public List<ColaboradorDto> ObtenerColaboradores()
        {
            
            var colaboradores = _colaboradorRepository.AsQueryable()
                .Include(c => c.Pais)
                .Include(c => c.Estado)
                .Include(c => c.Ciudad)
                .Select(c => new ColaboradorDto
                {
                    ColaboradorId = c.ColaboradorId,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    Email = c.Email,
                    Telefono = c.Telefono,
                    Pais = new PaisDto
                    {
                        PaisId = c.Pais.PaisId,
                        Nombre = c.Pais.Nombre
                    },
                    Estado = new EstadoDto
                    {
                        EstadoId = c.Estado.EstadoId,
                        Nombre = c.Estado.Nombre
                    },
                    Ciudad = new CiudadDto
                    {
                        CiudadId = c.Ciudad.CiudadId,
                        Nombre = c.Ciudad.Nombre
                    },
                    Latitud = c.Latitud,
                    Longitud = c.Longitud,
                    Activo = c.Activo
                })
                .ToList();

            return colaboradores;
        }

        public async Task<ErrorOr<string>> CrearColaborador(ColaboradorDto colaborador)
        {
           
            var validationResult = _validatorDomain.ValidateColaboradorForCreation(colaborador);
            if (validationResult.IsError)
            {
                return validationResult.FirstError;
            }

            var validationResultEmail = _validatorEmailDomain.ValidateEmail(colaborador.Email);
            if(validationResultEmail.IsError)
            {
                return validationResultEmail.FirstError;
            }

            bool correoExiste = _colaboradorRepository.AsQueryable().Where(c => c.Email.Equals(colaborador.Email)).Any();

            if (correoExiste)
            {
                return Error.Failure("Correo.YaExiste", "El correo ya esta asociado a un colaborador");
            }
            try
            {
                var newColaborador = new Colaborador
                {
                    Nombre = colaborador.Nombre,
                    Apellido = colaborador.Apellido,
                    Email = colaborador.Email,
                    Telefono = colaborador.Telefono,
                    Latitud = colaborador.Latitud,
                    Longitud = colaborador.Longitud,
                    PaisId = colaborador.Pais.PaisId,
                    EstadoId = colaborador.Estado.EstadoId,
                    CiudadId = colaborador.Ciudad.CiudadId,
                    Activo = true,
                    Fechacreacion = DateTime.UtcNow,
                    Fechaactualizacion = DateTime.UtcNow
                };

                _colaboradorRepository.Add(newColaborador);
                await _unitOfWork.SaveChangesAsync();

                return "Colaborador creado correctamente";
            }
            catch (Exception ex)
            {
                return Error.Failure("CrearColaborador.Failure", ex.Message);
            }
        }

        public async Task<ErrorOr<string>> EliminarColaborador(int id)
        {
            var colaborador = await _colaboradorRepository.FirstOrDefaultAsync(c => c.ColaboradorId == id);
            if (colaborador == null)
            {
                return Error.NotFound("Colaborador.NotFound", "El colaborador que intenta eliminar no se encontró.");
            }

            _colaboradorRepository.Remove(colaborador);
            await _unitOfWork.SaveChangesAsync();

            return "Colaborador eliminado correctamente";
        }

        public async Task<ErrorOr<string>> DesactivarColaborador(int id)
        {
            var colaborador = await _colaboradorRepository.FirstOrDefaultAsync(c => c.ColaboradorId == id);
            if (colaborador == null)
            {
                return Error.NotFound("General.NotFound", "El colaborador que intenta desactivar no se encontró.");
            }

            colaborador.Activo = false;
            await _unitOfWork.SaveChangesAsync();

            return "Colaborador desactivado correctamente";
        }

        public async Task<ErrorOr<string>> EditarColaborador(int id, EditarColaboradorDto infoColaboradorEditar)
        {
            var colaborador = await _colaboradorRepository.FirstOrDefaultAsync(c => c.ColaboradorId == id);
            if (colaborador == null)
            {
                return Error.NotFound("General.NotFound", "El colaborador que intenta editar no se encontró.");
            }

            var validationResult = _validatorDomain.ValidateColaboradorForUpdate(infoColaboradorEditar);
            if (validationResult.IsError)
            {
                return validationResult.FirstError;
            }

            colaborador.Nombre = infoColaboradorEditar.Nombre;
            colaborador.Apellido = infoColaboradorEditar.Apellido;
            colaborador.Email = infoColaboradorEditar.Email;
            colaborador.Telefono = infoColaboradorEditar.Telefono;
            colaborador.Fechaactualizacion = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return "Colaborador actualizado correctamente";
        }
    }
}