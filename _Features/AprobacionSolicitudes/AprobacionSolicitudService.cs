
using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common.StrategyPais;
using Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes.Dtos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes;
using System.Data.Common;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes
{
    public class AprobacionSolicitudService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AprobacionesSolicitud> _aprobacionSolicitud;
        private readonly IRepository<SolicitudViaje> _solicitudViaje;
        private readonly IRepository<Viaje> _viajes;
        private readonly IRepository<DetalleViaje> _detalleViajes;
        private readonly IRepository<ColaboradorSucursal> _colaboradorSucursal;
        private readonly IMonedaStrategy _monedaStrategy;
        private readonly AprobacionSolicitudAppDomain _validatorDomain;
        private readonly ViajeService _viajeService;

        public AprobacionSolicitudService(IUnitOfWork unitOfWork,
            IMonedaStrategy monedaStrategy,
            AprobacionSolicitudAppDomain validator,
            ViajeService viajeService )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork), "La unidad de trabajo no puede ser nula");
            _monedaStrategy = monedaStrategy ?? throw new ArgumentNullException(nameof(monedaStrategy), "La estrategia de moneda no puede ser nula");
            _validatorDomain = validator ?? throw new ArgumentNullException(nameof(validator), "El validador no puede ser nulo");
            _aprobacionSolicitud = unitOfWork.Repository<AprobacionesSolicitud>();
            _solicitudViaje = unitOfWork.Repository<SolicitudViaje>();
            _viajes = unitOfWork.Repository<Viaje>();
            _detalleViajes = unitOfWork.Repository<DetalleViaje>();
            _colaboradorSucursal = unitOfWork.Repository<ColaboradorSucursal>();
            _viajeService = viajeService;
        }


        public async Task<ErrorOr<string>> GestionarSolicitudViaje(AprobacionSolicitudDto request)
        {
            var initialValidation = _validatorDomain.ValidateSingleRequest(request);
            if (initialValidation.IsError)
                return initialValidation.FirstError;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                SolicitudViaje? solicitudviaje = await _solicitudViaje.FirstOrDefaultAsync(x => x.SolicitudViajeId == request.SolicitudId);
                var solicitudValidation = _validatorDomain.ValidateSolicitudViaje(solicitudviaje, request.SolicitudId, request.EstadoSolicitudId);
                if (solicitudValidation.IsError)
                    return solicitudValidation.FirstError;

                solicitudviaje.EstadoSolicitudId = request.EstadoSolicitudId;

                if (request.EstadoSolicitudId == 3) // Aprobada
                {
                    Viaje? viajeExistente = _viajes
                        .Where(v => v.SucursalId == solicitudviaje.SucursalId && v.FechaViaje.Date == 
                        solicitudviaje.FechaSolicitud.Date && v.DistanciaTotalKm<=100)
                        .FirstOrDefault();

                    int viajeId;

                    decimal totalDistance = 0;
                    if(viajeExistente != null)
                    {
                        totalDistance = viajeExistente.DistanciaTotalKm;
                    }

                  

                    if (viajeExistente == null || totalDistance>=100)
                    {
                        int monedaId = _monedaStrategy.GetMonedaID();
                        Viaje nuevoViaje = new Viaje
                        {
                            ViajeId = 0,
                            SucursalId = solicitudviaje.SucursalId,
                            TransportistaId = 2,
                            FechaViaje = DateTime.Now,
                            UsuarioRegistroId = request.GerenteId,
                            Activo = true,
                            DistanciaTotalKm = 0,
                            CostoTotal = 0,
                            MonedaId = monedaId,
                            Fechacreacion = DateTime.Now,
                            Fechaactualizacion = DateTime.Now
                        };

                        _viajes.Add(nuevoViaje);
                        await _unitOfWork.SaveChangesAsync();
                        viajeId = nuevoViaje.ViajeId;

                        var viajeValidation = _validatorDomain.ValidateViajeCreation(monedaId, viajeId, request.SolicitudId);
                        if (viajeValidation.IsError)
                            return viajeValidation.FirstError;
                    }
                    else
                    {
                        viajeId = viajeExistente.ViajeId;
                    }

                    ColaboradorSucursal? colaboradorSucursal = _colaboradorSucursal
                        .Where(cs => cs.ColaboradorId == solicitudviaje.ColaboradorId && cs.SucursalId == solicitudviaje.SucursalId)
                        .FirstOrDefault();

                    var colaboradorValidation = _validatorDomain.ValidateColaboradorSucursal(colaboradorSucursal, request.SolicitudId);
                    if (colaboradorValidation.IsError)
                        return colaboradorValidation.FirstError;

                    var detalleViaje = new DetalleViaje
                    {
                        ViajeId = viajeId,
                        ColaboradorSucursalId = colaboradorSucursal.ColaboradorSucursalId,
                        Fechacreacion = DateTime.Now,
                        Fechaactualizacion = DateTime.Now
                    };

                    await _viajeService.ActualizarViajes(DateTime.Now);
                    await _detalleViajes.AddAsync(detalleViaje);
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return "Solicitud de viaje gestionada correctamente";
            }
            catch (DbException ex)
            {
                await _unitOfWork.RollBackAsync();
                return Error.Failure("Error en la base de datos al gestionar la solicitud de viaje", ex.Message);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackAsync();
                return Error.Failure("Error inesperado al gestionar la solicitud de viaje", ex.Message);
            }
        }

        public async Task<ErrorOr<string>> GestionarSolicitudesViaje(List<AprobacionSolicitudDto> requests)
        {
            var initialValidation = _validatorDomain.ValidateMultipleRequests(requests);
            if (initialValidation.IsError)
                return initialValidation.FirstError;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                foreach (var request in requests)
                {
                    SolicitudViaje? solicitudViaje = await _solicitudViaje
                        .FirstOrDefaultAsync(x => x.SolicitudViajeId == request.SolicitudId);

                    var solicitudValidation = _validatorDomain.ValidateSolicitudViaje(solicitudViaje, request.SolicitudId, request.EstadoSolicitudId);
                    if (solicitudValidation.IsError)
                        return solicitudValidation.FirstError;

                    solicitudViaje.EstadoSolicitudId = request.EstadoSolicitudId;

                    if (request.EstadoSolicitudId == 3) 
                    {
                        Viaje? viajeExistente = await _viajes
                            .FirstOrDefaultAsync(v => v.SucursalId == solicitudViaje.SucursalId &&
                                                    v.FechaViaje.Date == solicitudViaje.FechaSolicitud.Date);

                        int viajeId;

                        if (viajeExistente == null)
                        {
                            int monedaId = _monedaStrategy.GetMonedaID();
                            Viaje nuevoViaje = new Viaje
                            {
                                ViajeId = 0,
                                SucursalId = solicitudViaje.SucursalId,
                                TransportistaId = 2,
                                FechaViaje = DateTime.Now,
                                UsuarioRegistroId = request.GerenteId,
                                Activo = true,
                                DistanciaTotalKm = 0,
                                CostoTotal = 0,
                                MonedaId = monedaId,
                                Fechacreacion = DateTime.Now,
                                Fechaactualizacion = DateTime.Now
                            };

                            _viajes.Add(nuevoViaje);
                            await _unitOfWork.SaveChangesAsync();
                            viajeId = nuevoViaje.ViajeId;

                            var viajeValidation = _validatorDomain.ValidateViajeCreation(monedaId, viajeId, request.SolicitudId);
                            if (viajeValidation.IsError)
                                return viajeValidation.FirstError;
                        }
                        else
                        {
                            viajeId = viajeExistente.ViajeId;
                        }

                        ColaboradorSucursal? colaboradorSucursal = await _colaboradorSucursal
                            .FirstOrDefaultAsync(cs => cs.ColaboradorId == solicitudViaje.ColaboradorId &&
                                                      cs.SucursalId == solicitudViaje.SucursalId);

                        var colaboradorValidation = _validatorDomain.ValidateColaboradorSucursal(colaboradorSucursal, request.SolicitudId);
                        if (colaboradorValidation.IsError)
                            return colaboradorValidation.FirstError;

                        var detalleViaje = new DetalleViaje
                        {
                            ViajeId = viajeId,
                            ColaboradorSucursalId = colaboradorSucursal.ColaboradorSucursalId,
                            Fechacreacion = DateTime.Now,
                            Fechaactualizacion = DateTime.Now
                        };

                        await _detalleViajes.AddAsync(detalleViaje);
                    }
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return "Solicitudes de viaje gestionadas correctamente";
            }
            catch (DbException ex)
            {
                await _unitOfWork.RollBackAsync();
                return Error.Failure("Error en la base de datos al procesar las solicitudes", ex.Message);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackAsync();
                return Error.Failure("Error inesperado al procesar las solicitudes de viaje", ex.Message);
            }
        }
    }
}