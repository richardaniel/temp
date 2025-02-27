using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Estados
{
    public class EstadoMap : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.HasKey(e => e.EstadoId).HasName("PK__Estados__053670B77B8E5D34");

            builder.HasIndex(e => new { e.Nombre, e.PaisId }, "UQ__Estados__0EAA4BB2C0684EB1").IsUnique();

            builder.Property(e => e.EstadoId).HasColumnName("estado_Id");
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            builder.Property(e => e.PaisId).HasColumnName("pais_Id");

            builder.HasOne(d => d.Pais).WithMany(p => p.Estados)
                .HasForeignKey(d => d.PaisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estados__pais_Id__412EB0B6");
        }

       
    }
}
