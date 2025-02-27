

using Microsoft.EntityFrameworkCore;
using Richar.Academia.ProyectoFinal.WebAPI._Features;
using Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Dispositivos;
using Richar.Academia.ProyectoFinal.WebAPI._Features.EstadoSolicitudes;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Evaluaciones;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Monedas;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Notificaciones;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Roles;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.AprobacionSolicitudes;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Ciudades;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.ColaboradorSucursales;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.DetallesViajes;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Dispositivos;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Estados;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.EstadoSolicitudes;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Evaluaciones;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Monedas;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Notificaciones;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Paises;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Roles;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.SolicitudesViaje;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Sucursales;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Transportistas;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Usuarios;
using Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Viajes;


namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte
{
    public class SistemaTransporteContext : DbContext
    {
        public SistemaTransporteContext (DbContextOptions<SistemaTransporteContext> options) : base(options) { }

        public virtual DbSet<AprobacionesSolicitud> AprobacionesSolicitudes { get; set; }

        public virtual DbSet<Ciudad> Ciudades { get; set; }

        public virtual DbSet<ColaboradorSucursal> ColaboradorSucursales { get; set; }

        public virtual DbSet<Colaborador> Colaboradores { get; set; }

        public virtual DbSet<DetalleViaje> DetalleViajes { get; set; }

        public virtual DbSet<Dispositivo> Dispositivos { get; set; }

        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<EstadoSolicitud> EstadoSolicitudes { get; set; }

        public virtual DbSet<Evaluacion> Evaluaciones { get; set; }

        public virtual DbSet<Moneda> Monedas { get; set; }

        public virtual DbSet<Notificacion> Notificaciones { get; set; }

        public virtual DbSet<Pais> Paises { get; set; }

        public virtual DbSet<Rol> Roles { get; set; }

        public virtual DbSet<SolicitudViaje> SolicitudesViajes { get; set; }

        public virtual DbSet<Sucursal> Sucursales { get; set; }

        public virtual DbSet<Transportista> Transportistas { get; set; }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<Viaje> Viajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ColaboradorSucursalMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new ColaboradorMap());
            modelBuilder.ApplyConfiguration(new SucursalMap());
            modelBuilder.ApplyConfiguration(new ViajeMap());
            modelBuilder.ApplyConfiguration(new DetalleViajeMap());
            modelBuilder.ApplyConfiguration(new TransportistaMap());
            modelBuilder.ApplyConfiguration(new CiudadMap());
            modelBuilder.ApplyConfiguration(new EstadoMap());
            modelBuilder.ApplyConfiguration(new PaisMap());
            modelBuilder.ApplyConfiguration(new MonedaMap());
            modelBuilder.ApplyConfiguration(new NotificacionMap());
            modelBuilder.ApplyConfiguration(new DispositivoMap());
            modelBuilder.ApplyConfiguration(new SolicitudViajeMap());
            modelBuilder.ApplyConfiguration(new AprobacionSolicitudMap());
            modelBuilder.ApplyConfiguration(new EvaluacionMap());
            modelBuilder.ApplyConfiguration(new EstadoSolicitudMap());







        }
    }
}
