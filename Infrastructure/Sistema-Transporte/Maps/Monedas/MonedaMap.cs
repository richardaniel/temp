using Microsoft.EntityFrameworkCore;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Monedas;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Monedas
{
    public class MonedaMap:IEntityTypeConfiguration <Moneda>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Moneda> builder)
        {
            builder.HasKey(e => e.MonedaId).HasName("PK__Monedas__8C96C3A54D41D8B8");

            builder.HasIndex(e => e.CodigoIso, "UQ__Monedas__296D3C10F7221E97").IsUnique();

            builder.HasIndex(e => e.Nombre, "UQ__Monedas__72AFBCC61BF66DB4").IsUnique();

            builder.Property(e => e.MonedaId).HasColumnName("moneda_Id");
            builder.Property(e => e.CodigoIso)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo_iso");
            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            builder.Property(e => e.PaisId).HasColumnName("pais_Id");
            builder.Property(e => e.Simbolo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("simbolo");

            builder.HasOne(d => d.Pais).WithMany(p => p.Moneda)
                .HasForeignKey(d => d.PaisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Monedas__pais_Id__49C3F6B7");
        }
    }
}
