using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Richar.Academia.ProyectoFinal.WebAPI._Features;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Viajes
{
    public class ViajeMap : IEntityTypeConfiguration<Viaje>
    {

        public void Configure(EntityTypeBuilder<Viaje> builder)
        {

            builder.HasKey(e => e.ViajeId).HasName("PK_viaje_Id");

            builder.HasIndex(e => e.FechaViaje, "IDX_Viajes_FechaViaje");

            builder.Property(e => e.ViajeId).HasColumnName("viaje_Id");
            builder.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            builder.Property(e => e.CostoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("costo_total");
            builder.Property(e => e.DistanciaTotalKm)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("distancia_total_km");
            builder.Property(e => e.FechaViaje).HasColumnName("fecha_viaje");
            builder.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaactualizacion");
            builder.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechacreacion");
            builder.Property(e => e.MonedaId).HasColumnName("moneda_Id");
            builder.Property(e => e.SucursalId).HasColumnName("sucursal_Id");
            builder.Property(e => e.TransportistaId).HasColumnName("transportista_Id");
            builder.Property(e => e.UsuarioRegistroId).HasColumnName("usuario_registro_Id");

            builder.HasOne(d => d.Moneda).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.MonedaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viajes__moneda_I__00200768");

            builder.HasOne(d => d.Sucursal).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viajes__sucursal__7D439ABD");

            builder.HasOne(d => d.Transportista).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.TransportistaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viajes__transpor__7E37BEF6");

            builder.HasOne(d => d.UsuarioRegistro).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.UsuarioRegistroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Viajes__usuario___7F2BE32F");
        }
    }
}
