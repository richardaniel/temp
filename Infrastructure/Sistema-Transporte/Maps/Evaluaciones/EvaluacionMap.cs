using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Richar.Academia.ProyectoFinal.WebAPI._Features.Evaluaciones;


namespace Richar.Academia.ProyectoFinal.WebAPI.Infrastructure.Sistema_Transporte.Maps.Evaluaciones
{
    public class EvaluacionMap : IEntityTypeConfiguration<Evaluacion>
    {
        public void Configure(EntityTypeBuilder<Evaluacion> builder)
        {
            builder.HasKey(e => e.EvaluacionId).HasName("PK_evaluacion_Id");

            builder.Property(e => e.EvaluacionId).HasColumnName("evaluacion_Id");
            builder.Property(e => e.Calificacion).HasColumnName("calificacion");
            builder.Property(e => e.ColaboradorId).HasColumnName("colaborador_Id");
            builder.Property(e => e.Evaluacion_)
                .HasColumnType("text")
                .HasColumnName("evaluacion");
            builder.Property(e => e.Evaluador)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("evaluador");
            builder.Property(e => e.Fechaactualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaactualizacion");
            builder.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechacreacion");
            builder.Property(e => e.ViajeId).HasColumnName("viaje_Id");

            builder.HasOne(d => d.Colaborador).WithMany(p => p.Evaluaciones)
                .HasForeignKey(d => d.ColaboradorId)
                .HasConstraintName("FK__Evaluacio__colab__245D67DE");

            builder.HasOne(d => d.Viaje).WithMany(p => p.Evaluaciones)
                .HasForeignKey(d => d.ViajeId)
                .HasConstraintName("FK__Evaluacio__viaje__25518C17");
        }
    }
}
