using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features;


namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Transportistas
{
    public class TransportistaMap:IEntityTypeConfiguration<Transportista>
    {
        

        public void Configure(EntityTypeBuilder<Transportista> builder)
        {
            builder.HasKey(e => e.TransportistaId).HasName("PK_transportista_Id");

            builder.Property(e => e.TransportistaId).HasColumnName("transportista_Id");
            builder.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            builder.Property(e => e.CiudadId).HasColumnName("ciudad_Id");
            builder.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            builder.Property(e => e.EstadoId).HasColumnName("estado_Id");
            builder.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaactualizacion");
            builder.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechacreacion");
            builder.Property(e => e.MonedaId).HasColumnName("moneda_Id");
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            builder.Property(e => e.PaisId).HasColumnName("pais_Id");
            builder.Property(e => e.TarifaKm)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("tarifa_km");

            builder.HasOne(d => d.Ciudad).WithMany(p => p.Transportista)
                .HasForeignKey(d => d.CiudadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transport__ciuda__6E01572D");

            builder.HasOne(d => d.Estado).WithMany(p => p.Transportista)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transport__estad__6D0D32F4");

            builder.HasOne(d => d.Moneda).WithMany(p => p.Transportista)
                .HasForeignKey(d => d.MonedaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transport__moned__6B24EA82");

            builder.HasOne(d => d.Pais).WithMany(p => p.Transportista)
                .HasForeignKey(d => d.PaisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transport__pais___6C190EBB");
        }
    }
}
