using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Ciudades;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Ciudades
{
    public class CiudadMap : IEntityTypeConfiguration<Ciudad>
    {
        public void Configure(EntityTypeBuilder<Ciudad> builder)
        {
            builder.HasKey(e => e.CiudadId).HasName("PK__Ciudades__AA05D75FE355EA24");

            builder.HasIndex(e => new { e.Nombre, e.EstadoId }, "UQ__Ciudades__12FCDBCDB86BBF8C").IsUnique();

            builder.Property(e => e.CiudadId).HasColumnName("ciudad_Id");
            builder.Property(e => e.EstadoId).HasColumnName("estado_Id");
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            builder.HasOne(d => d.Estado).WithMany(p => p.Ciudades)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ciudades__estado__44FF419A");
        }
    }
}
