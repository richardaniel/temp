using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features.SolicitudesViaje;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.SolicitudesViaje
{
    public class SolicitudViajeMap : IEntityTypeConfiguration<SolicitudViaje>
    {
        public void Configure(EntityTypeBuilder<SolicitudViaje> builder)
        {
            builder.HasKey(e => e.SolicitudViajeId).HasName("PK_solicitudViaje_Id");

            builder.ToTable("SolicitudesViaje");

            builder.HasIndex(e => e.FechaSolicitud, "IDX_SolicitudesViaje_FechaSolicitud");

            builder.Property(e => e.SolicitudViajeId).HasColumnName("solicitudViaje_Id");
            builder.Property(e => e.ColaboradorId).HasColumnName("colaborador_Id");
            builder.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            builder.Property(e => e.EstadoSolicitudId).HasColumnName("estadoSolicitud_Id");
            builder.Property(e => e.FechaSolicitud)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_solicitud");
            builder.Property(e => e.SucursalId).HasColumnName("sucursal_Id");

            builder.HasOne(d => d.Colaborador).WithMany(p => p.SolicitudesViajes)
                .HasForeignKey(d => d.ColaboradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Solicitud__colab__17036CC0");

            builder.HasOne(d => d.EstadoSolicitud).WithMany(p => p.SolicitudesViajes)
                .HasForeignKey(d => d.EstadoSolicitudId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Solicitud__estad__18EBB532");

            builder.HasOne(d => d.Sucursal).WithMany(p => p.SolicitudesViajes)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Solicitud__sucur__17F790F9");
        }
    }
}
