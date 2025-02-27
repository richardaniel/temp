using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Notificaciones;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Notificaciones
{
    public class NotificacionMap : IEntityTypeConfiguration<Notificacion>


    {
        public void Configure(EntityTypeBuilder<Notificacion> builder)
        {
            builder.HasKey(e => e.NotificacionId).HasName("PK_notificacion_Id");

            builder.Property(e => e.NotificacionId).HasColumnName("notificacion_Id");
            builder.Property(e => e.FechaNotificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_notificacion");
            builder.Property(e => e.Mensaje)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mensaje");
            builder.Property(e => e.ReceptorTipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("receptor_tipo");
            builder.Property(e => e.TransportistaId).HasColumnName("transportista_Id");
            builder.Property(e => e.UsuarioId).HasColumnName("usuario_Id");

            builder.HasOne(d => d.Transportista).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.TransportistaId)
                .HasConstraintName("FK_Notificaciones_Transportistas");

            builder.HasOne(d => d.Usuario).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Notificaciones_Usuarios");
        }
    }
}
