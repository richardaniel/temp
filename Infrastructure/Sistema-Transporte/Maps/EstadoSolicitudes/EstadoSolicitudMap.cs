using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features.EstadoSolicitudes;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.EstadoSolicitudes
{
    public class EstadoSolicitudMap : IEntityTypeConfiguration<EstadoSolicitud>
    {
        public void Configure(EntityTypeBuilder<EstadoSolicitud> builder)
        {
            builder.HasKey(e => e.EstadoSolicitudId).HasName("PK__EstadoSo__40C15CDF75CB08D4");

            builder.HasIndex(e => e.Nombre, "UQ__EstadoSo__75E3EFCF05420374").IsUnique();

            builder.Property(e => e.EstadoSolicitudId).HasColumnName("estadoSolicitud_Id");
            builder.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}
