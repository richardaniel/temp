using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using Richar.Academia.ProyectoFinal.WebAPI._Features;


namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.DetallesViajes
{
    public class DetalleViajeMap : IEntityTypeConfiguration<DetalleViaje>
    {
        public void Configure(EntityTypeBuilder<DetalleViaje> builder)
        {
            builder.HasKey(e => e.DetalleViajeId).HasName("PK_detalleViaje_Id");

            builder.Property(e => e.DetalleViajeId).HasColumnName("detalleViaje_Id");
            builder.Property(e => e.ColaboradorSucursalId).HasColumnName("colaboradorSucursal_Id");
            builder.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaactualizacion");
            builder.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechacreacion");
            builder.Property(e => e.ViajeId).HasColumnName("viaje_Id");

            builder.HasOne(d => d.ColaboradorSucursal).WithMany(p => p.DetalleViajes)
                .HasForeignKey(d => d.ColaboradorSucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleVi__colab__05D8E0BE");

            builder.HasOne(d => d.Viaje).WithMany(p => p.DetalleViajes)
                .HasForeignKey(d => d.ViajeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleVi__viaje__04E4BC85");
        }
    }
}
