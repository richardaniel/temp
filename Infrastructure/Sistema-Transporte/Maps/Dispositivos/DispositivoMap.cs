using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Dispositivos;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Dispositivos
{
    public class DispositivoMap : IEntityTypeConfiguration<Dispositivo>
    {
        public void Configure(EntityTypeBuilder<Dispositivo> builder)
        {
            builder.HasKey(e => e.DispositivoId).HasName("PK_dispositivo_Id");

            builder.Property(e => e.DispositivoId).HasColumnName("dispositivo_Id");
            builder.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            builder.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            builder.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("token");
            builder.Property(e => e.UsuarioId).HasColumnName("usuario_Id");

            builder.HasOne(d => d.Usuario).WithMany(p => p.Dispositivos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dispositi__usuar__0A9D95DB");
        }
    }
}
