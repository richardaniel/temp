using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features;


namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.ColaboradorSucursales
{
    public class ColaboradorSucursalMap : IEntityTypeConfiguration<ColaboradorSucursal>
    {
        public void Configure(EntityTypeBuilder<ColaboradorSucursal> builder)
        {

            builder.HasKey(e => e.ColaboradorSucursalId).HasName("PK_colaboradorSucursal_Id");

            builder.ToTable("ColaboradorSucursal");

            builder.HasIndex(e => new { e.ColaboradorId, e.SucursalId }, "UQ__Colabora__9806C3D1968BAE2D").IsUnique();

            builder.Property(e => e.ColaboradorSucursalId).HasColumnName("colaboradorSucursal_Id");
            builder.Property(e => e.ColaboradorId).HasColumnName("colaborador_Id");
            builder.Property(e => e.DistanciaKm)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("distancia_km");
            builder.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaactualizacion");
            builder.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechacreacion");
            builder.Property(e => e.SucursalId).HasColumnName("sucursal_Id");

            builder.HasOne(d => d.Colaborador).WithMany(p => p.ColaboradorSucursals)
                .HasForeignKey(d => d.ColaboradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Colaborad__colab__74AE54BC");

            builder.HasOne(d => d.Sucursal).WithMany(p => p.ColaboradorSucursals)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Colaborad__sucur__75A278F5");
        }
    }
}
