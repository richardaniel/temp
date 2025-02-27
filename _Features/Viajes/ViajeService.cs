using ErrorOr;
using Farsiman.Domain.Core.Standard.Repositories;

using Richar.Academia.ProyectoFinal.WebAPI._Features._Common;
using Richar.Academia.ProyectoFinal.WebAPI._Features._Common.StrategyPais;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes.Dtos;



namespace Richar.Academia.ProyectoFinal.WebAPI._Features.Viajes
{
    public class ViajeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Viaje> _viajeRepository;
        private readonly IRepository<DetalleViaje> _detalleViajeRepository;
        private readonly IRepository<Sucursal> _sucursalesRepository;
        private readonly IRepository<ColaboradorSucursal> _colaboradorSucursalRepository;
        private readonly IRepository<Colaborador> _colaboradorRepository;
        private readonly IMonedaStrategy _monedaStrategy;



        private readonly RutasService _servicioRutas;

        public ViajeService(IUnitOfWork unitOfWork, RutasService servicioRutas ,IMonedaStrategy monedaStrategy)
        {
            _unitOfWork = unitOfWork;
            _colaboradorRepository = unitOfWork.Repository<Colaborador>();
            _viajeRepository = unitOfWork.Repository<Viaje>();
            _detalleViajeRepository = unitOfWork.Repository<DetalleViaje>();
            _sucursalesRepository = unitOfWork.Repository<Sucursal>();
            _colaboradorSucursalRepository = unitOfWork.Repository<ColaboradorSucursal>();
            _servicioRutas = servicioRutas;
            _monedaStrategy = monedaStrategy;
        }

        public async Task<ErrorOr<string>> CrearViaje(ViajeDto viajedto)
        {
            try
            {
                int monedaId = _monedaStrategy.GetMonedaID(); 
                Viaje viaje = new Viaje
                {
                    ViajeId = 0,
                    SucursalId = viajedto.SucursalId,
                    TransportistaId = viajedto.TransportistaId,
                    FechaViaje = DateTime.Now,
                    UsuarioRegistroId = viajedto.UsuarioRegistroId,

                    Activo = true,

                    DistanciaTotalKm = viajedto.DistanciaTotalKm,

                    CostoTotal = viajedto.CostoTotal,

                    MonedaId = monedaId,

                    Fechacreacion = DateTime.Now,

                    Fechaactualizacion = DateTime.Now
                };


                await _viajeRepository.AddAsync(viaje);
                await _unitOfWork.SaveChangesAsync();

                return "viaje asignado correctamente";

            }
            catch (Exception ex)
            {
                return Error.Failure("Error al crear el viaje", ex.Message);
            }
            return "";
        }

        public async Task<ErrorOr<string>> ActualizarViajes(DateTime FechaViaje)
        {
            DateTime fechaInicio = FechaViaje.Date;
            DateTime fechaFin = fechaInicio.AddDays(1);
           
            List<DetalleViaje> detallesViajes = new List<DetalleViaje>();
            List<Coordenada> coordenadas = new List<Coordenada>();
            try
            {
                List<Viaje> viajes = _viajeRepository.AsQueryable().Where(x => x.FechaViaje >= fechaInicio && x.FechaViaje < fechaFin).ToList();
                              

                foreach (Viaje viaje in viajes)
                {
                     detallesViajes = _detalleViajeRepository.Where(x => x.ViajeId == viaje.ViajeId)
                              .Select(x => new DetalleViaje
                              {
                                  DetalleViajeId = x.DetalleViajeId,
                                  ViajeId = x.ViajeId,
                                  ColaboradorSucursalId = x.ColaboradorSucursalId,
                                  Fechacreacion = x.Fechacreacion,
                                  Fechaactualizacion = DateTime.Now
                              }).ToList();


                    var coordenadasCasas = new List<(decimal Latitud, decimal Longitud)>();


                    foreach (var detalleviaje in detallesViajes)
                    {
                        var colaboradorSucursal = await _colaboradorSucursalRepository
                        .FirstOrDefaultAsync(cs => cs.ColaboradorSucursalId == detalleviaje.ColaboradorSucursalId);

                        if (colaboradorSucursal != null)
                        {
                            
                            var colaborador = await _colaboradorRepository
                                .FirstOrDefaultAsync(c => c.ColaboradorId == colaboradorSucursal.ColaboradorId);

                            if (colaborador != null)
                            {
                                coordenadasCasas.Add((colaborador.Latitud, colaborador.Longitud));
                            }
                        }
                    }
                    Sucursal? sucursal = await _sucursalesRepository.FirstOrDefaultAsync(s => s.SucursalId == viaje.SucursalId);

                    decimal distanciaTotal = await _servicioRutas.CalcularRutaOptima(coordenadasCasas);
                   
                    viaje.DistanciaTotalKm = distanciaTotal;
                    viaje.Fechaactualizacion = DateTime.Now;

                    _viajeRepository.Update(viaje);
                    await _unitOfWork.SaveChangesAsync();
                    
                }

                
                    return "viaje actualizado correctamente";
            }
            catch (Exception ex)
            {
                return Error.Failure("Error al actualizar el viaje", ex.Message);
            }
        }

    }
}
