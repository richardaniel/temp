using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features.AprobacionSolicitudes;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.AprobacionSolicitudes
{
    public class AprobacionSolicitudMap : IEntityTypeConfiguration<AprobacionesSolicitud>
    {
        public void Configure(EntityTypeBuilder<AprobacionesSolicitud> builder)
        {
            builder.HasKey(e => e.AprobacionSolicitudId).HasName("PK_aprobacionSolicitud_Id");

            builder.Property(e => e.AprobacionSolicitudId).HasColumnName("aprobacionSolicitud_Id");
            builder.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            builder.Property(e => e.EstadoSolicitudId).HasColumnName("estadoSolicitud_Id");
            builder.Property(e => e.FechaAprobacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_aprobacion");
            builder.Property(e => e.GerenteId).HasColumnName("gerente_Id");
            builder.Property(e => e.SolicitudId).HasColumnName("solicitud_Id");

            builder.HasOne(d => d.EstadoSolicitud).WithMany(p => p.AprobacionesSolicitudes)
                .HasForeignKey(d => d.EstadoSolicitudId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Aprobacio__estad__1EA48E88");

            builder.HasOne(d => d.Gerente).WithMany(p => p.AprobacionesSolicitudes)
                .HasForeignKey(d => d.GerenteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Aprobacio__geren__1DB06A4F");

            builder.HasOne(d => d.Solicitud).WithMany(p => p.AprobacionesSolicitudes)
                .HasForeignKey(d => d.SolicitudId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Aprobacio__solic__1CBC4616");
        }
    }
}
