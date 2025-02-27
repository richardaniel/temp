using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Colaboradores;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Roles;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Usuarios;

namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Usuarios
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>

    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.UsuarioId).HasName("PK_usuario_Id");

            builder.HasIndex(e => e.ColaboradorId, "UQ_colaborador_Id").IsUnique();

            builder.Property(e => e.UsuarioId).HasColumnName("usuario_Id");
            builder.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            builder.Property(e => e.ColaboradorId).HasColumnName("colaborador_Id");
            builder.Property(e => e.Contrasena)
                .HasMaxLength(32)
                .HasColumnName("contrasena");
            builder.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaactualizacion");
            builder.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechacreacion");
            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            builder.Property(e => e.RolId).HasColumnName("rol_id");

            builder.HasOne(d => d.Colaborador).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.ColaboradorId)
                .HasConstraintName("FK__Usuarios__colabo__5DCAEF64");

            builder.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__rol_id__5CD6CB2B");

        }
    }
}
