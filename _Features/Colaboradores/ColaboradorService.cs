using AutoMapper;
using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Helpers;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;
namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores
{
    public class ColaboradorService
    {
        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ColaboradorAppDomain _validatorDomain;
        private readonly IRepository<Colaborador> _colaboradorRepository;
        private readonly IGenericValidatorService _genericValidatorService;
       

        public ColaboradorService(IMapper mapper,ColaboradorAppDomain validator ,IUnitOfWork unitOfWork , IGenericValidatorService genericValidatorService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validatorDomain = validator;
            _colaboradorRepository = unitOfWork.Repository<Colaborador>();
            _genericValidatorService = genericValidatorService;
        }

        public List<ColaboradorDtoResponse> ObtenerColaboradores()
        {

            var colaboradores = _colaboradorRepository.AsQueryable()
                .Include(p => p.Pais)
                .Include(e => e.Estado)
                .Include(c => c.Ciudad)
                .ToList();
            return _mapper.Map<List<ColaboradorDtoResponse>>(colaboradores);
        }

        public async Task<ErrorOr<string>> CrearColaborador(ColaboradorDtoRequest colaboradorDto)
        {
           Colaborador colaborador = _mapper.Map<Colaborador>(colaboradorDto);


            bool paisExiste = await _genericValidatorService.ExistsAsync<Pais>(colaborador.PaisId);

            bool estadoExiste = await _genericValidatorService.ExistsAsync<Estado>(colaborador.EstadoId);

            bool ciudadExiste = await _genericValidatorService.ExistsAsync<Ciudad>(colaborador.CiudadId);

            bool correoExiste = await _genericValidatorService.IsEmailAsociado<Colaborador>(colaborador.Email);

            ColaboradorDomainRequeriment colaboradorDomainRequeriment = ColaboradorDomainRequeriment.Fill(paisExiste, estadoExiste, ciudadExiste, correoExiste);

            ErrorOr<Colaborador> colaboradorValido = _validatorDomain.ValidateColaboradorForCreation(colaborador , colaboradorDomainRequeriment);

            if (colaboradorValido.IsError)
            {

                return colaboradorValido.FirstError;

            }

            try
            {
               
                _colaboradorRepository.Add(colaboradorValido.Value);
                await _unitOfWork.SaveChangesAsync();

                return string.Format(MensajesGlobales.EntidadCreadaCorrectamente ,"colaborador");
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