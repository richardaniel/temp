using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;



namespace Richar.Academia.ProyectoFinal.WebAPI._Features.ColaboradoresSucursales
{
    public class ColaboradorSucursalService
    {
        private readonly IUnitOfWork _unitOfWork;
      
        private readonly LocationService _locationService;
        private readonly ColaboradorSucursalAppDomain _validator;
        private readonly IRepository<Colaborador> _colaboradores;
        private readonly IRepository<Sucursal> _sucursales;
        private readonly IRepository<ColaboradorSucursal> _colaboradorSucursales;

        public ColaboradorSucursalService(
            IUnitOfWork unitOfWork,
            LocationService locationService,
            ColaboradorSucursalAppDomain validator)
        {
            _unitOfWork = unitOfWork;
            _locationService = locationService;
            _validator = validator;
            _colaboradores = unitOfWork.Repository<Colaborador>();
            _sucursales = unitOfWork.Repository<Sucursal>();
            _colaboradorSucursales = unitOfWork.Repository<ColaboradorSucursal>();
        }

        public async Task<ErrorOr<string>> AsignarColaboradorASucursal(ColaboradorSucursalDto colaboradorSucursalDto)
        {
            try
            {
               
                Colaborador? colaborador = await _colaboradores.FirstOrDefaultAsync(x => x.ColaboradorId == colaboradorSucursalDto.ColaboradorId);
                Sucursal? sucursal = await _sucursales.FirstOrDefaultAsync(x => x.SucursalId == colaboradorSucursalDto.SucursalId);

                
                var assignmentValidation = _validator.ValidateAssignment(colaboradorSucursalDto, colaborador, sucursal);
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
                var colaboradorSucursal = new ColaboradorSucursal
                {
                    ColaboradorId = colaborador.ColaboradorId,
                    SucursalId = sucursal.SucursalId,
                    DistanciaKm = distance
                };

                //_unitOfWork.Repository<ColaboradorSucursales>.Add(colaboradorSucursal);
                _colaboradorSucursales.Add(colaboradorSucursal);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return distance.ToString();
            }
            catch (Exception exception)
            {
                await _unitOfWork.RollBackAsync();
                return Error.Failure("AsignarColaborador.Failure", exception.Message);
            }
        }

        public async Task<ErrorOr<string>> ObtenerColaboradoresAsignados()
        {
            
            return "Colaboradores asignados";
        }
    }
}