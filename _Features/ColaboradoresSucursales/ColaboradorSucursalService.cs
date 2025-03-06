using AutoMapper;
using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales.Dtos;
using System.Runtime.CompilerServices;



namespace Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales
{
    public class ColaboradorSucursalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LocationService _locationService;
        private readonly ColaboradorSucursalAppDomain _validator;
        private readonly IRepository<Colaborador> _colaboradores;
        private readonly IRepository<Sucursal> _sucursales;
        private readonly IRepository<ColaboradorSucursal> _colaboradorSucursales;

        public ColaboradorSucursalService(
            IUnitOfWork unitOfWork,
            LocationService locationService,
            ColaboradorSucursalAppDomain validator,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _locationService = locationService;
            _validator = validator;
            _colaboradores = unitOfWork.Repository<Colaborador>();
            _sucursales = unitOfWork.Repository<Sucursal>();
            _colaboradorSucursales = unitOfWork.Repository<ColaboradorSucursal>();
            _mapper = mapper;
        }

        public async Task<ErrorOr<string>> AsignarColaboradorASucursal(ColaboradorSucursalDtoRequest colaboradorSucursalDtoRequest)
        {
            try
            {
               
                Colaborador? colaborador = await _colaboradores.FirstOrDefaultAsync(x => x.ColaboradorId == colaboradorSucursalDtoRequest.ColaboradorId);
                Sucursal? sucursal = await _sucursales.FirstOrDefaultAsync(x => x.SucursalId == colaboradorSucursalDtoRequest.SucursalId);


                ColaboradorSucursal colaboradorSucursal = _mapper.Map<ColaboradorSucursal>(colaboradorSucursalDtoRequest);
                
                var assignmentValidation = _validator.ValidateAssignment(colaboradorSucursal, colaborador, sucursal);
                if (assignmentValidation.IsError)
                {
                    return assignmentValidation.FirstError;
                }

               Point points = new Point();
                points.OrginLat = colaborador.Latitud;
                points.OrginLon = colaborador.Longitud;
                points.DestinationLat = sucursal.Latitud;
                points.DestinationLon = sucursal.Longitud;

                DistanceMatrixResponse distanceKm = await _locationService.GetDistanceAsync(
                   points);

                var distanceValidation = _validator.ValidateDistance(distanceKm);
                if (distanceValidation.IsError)
                {
                    return distanceValidation.FirstError;
                }

                decimal distance = distanceValidation.Value;


                await _unitOfWork.BeginTransactionAsync();
                var newColaboradorSucursal = new ColaboradorSucursal
                {
                    ColaboradorId = colaborador.ColaboradorId,
                    SucursalId = sucursal.SucursalId,
                    DistanciaKm = distance
                };

                //_unitOfWork.Repository<ColaboradorSucursales>.Add(colaboradorSucursal);
                _colaboradorSucursales.Add(newColaboradorSucursal);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return $"Colaborador Asignado , la distacia es {distance.ToString()} KM";
            }
            catch (Exception exception)
            {
                await _unitOfWork.RollBackAsync();
                return Error.Failure("AsignarColaborador.Failure", exception.Message);
            }
        }

        public async Task<ErrorOr<List<ColaboradorAsignadoDto>>> ObtenerColaboradoresAsignados()
        {

            List<ColaboradorAsignadoDto> colaboradores =await  _colaboradorSucursales
                .AsQueryable()
                .Include(cs => cs.Colaborador)
                .Include(cs=>cs.Sucursal)
                .Select(
                cs => new ColaboradorAsignadoDto
                {

                    ColaboradorId = cs.ColaboradorId,
                    Distancia_vivienda =cs.DistanciaKm,
                    sucursal = new SucursalDto
                   {
                       Nombre = cs.Sucursal.Nombre,
                       Latitud = cs.Sucursal.Latitud,
                       Longitud = cs.Sucursal.Longitud,
                       Direccion = cs.Sucursal.Direccion


                   },
                   colaborador = new ColaboradorDtoRequest
                   {
                       Nombre = cs.Colaborador.Nombre,
                       Latitud = cs.Colaborador.Latitud,
                       Longitud =cs.Colaborador.Longitud,
                       Email = cs.Colaborador.Email,
                       



                   }
                }
                ).ToListAsync();
            
            return colaboradores;
        }
    }
}