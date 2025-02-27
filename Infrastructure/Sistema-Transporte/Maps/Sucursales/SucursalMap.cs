using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Sucursales;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Sucursales
{
    public class SucursalMap : IEntityTypeConfiguration<Sucursal>
    {
        public void Configure(EntityTypeBuilder<Sucursal> builder)
        {

            builder.HasKey(e => e.SucursalId).HasName("PK_sucursal_Id");

            builder.Property(e => e.SucursalId).HasColumnName("sucursal_Id");
            builder.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            builder.Property(e => e.CiudadId).HasColumnName("ciudad_Id");
            builder.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            builder.Property(e => e.EstadoId).HasColumnName("estado_Id");
            builder.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaactualizacion");
            builder.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechacreacion");
            builder.Property(e => e.Latitud)
                .HasColumnType("decimal(10, 8)")
                .HasColumnName("latitud");
            builder.Property(e => e.Longitud)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("longitud");
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            builder.Property(e => e.PaisId).HasColumnName("pais_Id");

            builder.HasOne(d => d.Ciudad).WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.CiudadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sucursale__ciuda__656C112C");

            builder.HasOne(d => d.Estado).WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sucursale__estad__6477ECF3");

            builder.HasOne(d => d.Pais).WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.PaisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sucursale__fecha__6383C8BA");


        }
    }
}
