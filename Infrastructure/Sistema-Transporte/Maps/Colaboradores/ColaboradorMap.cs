using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Colaboradores
{
    public class ColaboradorMap : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.HasKey(e => e.ColaboradorId).HasName("PK_colaborador_Id");

            builder.HasIndex(e => e.Email, "IDX_Colaboradores_Email");

            builder.HasIndex(e => e.Telefono, "IDX_Colaboradores_Telefono");

            builder.HasIndex(e => e.Email, "UQ_Colaboradores_Email").IsUnique();

            builder.Property(e => e.ColaboradorId).HasColumnName("colaborador_Id");
            builder.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            builder.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            builder.Property(e => e.CiudadId).HasColumnName("ciudad_Id");

            builder.Property(e => e.Email)
                .HasMaxLength(100)
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

            builder.Property(e => e.Latitud)
                .HasColumnType("decimal(10, 8)")
                .HasColumnName("latitud");

            builder.Property(e => e.Longitud)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("longitud");

            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            builder.Property(e => e.PaisId).HasColumnName("pais_Id");

            builder.Property(e => e.Telefono).HasColumnName("telefono");

            builder.HasOne(d => d.Ciudad).WithMany(p => p.Colaboradores)
                .HasForeignKey(d => d.CiudadId)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Colaborad__ciuda__5535A963");

            builder.HasOne(d => d.Estado).WithMany(p => p.Colaboradores)
                .HasForeignKey(d => d.EstadoId)
                 //.OnDelete(DeleteBehavior.ClientSetNull)
                 .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Colaborad__estad__5441852A");

            builder.HasOne(d => d.Pais).WithMany(p => p.Colaboradores)
                .HasForeignKey(d => d.PaisId)
                 //.OnDelete(DeleteBehavior.ClientSetNull)
                 .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Colaborad__pais___534D60F1");

            

        }
    }
}
