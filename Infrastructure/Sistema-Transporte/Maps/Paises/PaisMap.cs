using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Paises;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Paises
{
    public class PaisMap : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.HasKey(e => e.PaisId).HasName("PK__Paises__C05F7746E92E0364");

            builder.HasIndex(e => e.CodigoIso, "UQ__Paises__296D3C102DF94F67").IsUnique();

            builder.HasIndex(e => e.Nombre, "UQ__Paises__72AFBCC67D7ECB40").IsUnique();

            builder.Property(e => e.PaisId).HasColumnName("pais_Id");
            builder.Property(e => e.CodigoIso)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo_iso");
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        }
    }
}
