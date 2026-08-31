using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Infrastructure.Persistence.Configurations;

public sealed class ConvocatoriaConfiguration : IEntityTypeConfiguration<Convocatoria>
{
    public void Configure(EntityTypeBuilder<Convocatoria> builder)
    {
        builder.ToTable("convocatorias");

        builder.HasKey(convocatoria => convocatoria.Id);

        builder.Property(convocatoria => convocatoria.Id)
            .HasColumnName("id");

        builder.Property(convocatoria => convocatoria.Titulo)
            .HasColumnName("titulo")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(convocatoria => convocatoria.Descripcion)
            .HasColumnName("descripcion")
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(convocatoria => convocatoria.Requisitos)
            .HasColumnName("requisitos")
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(convocatoria => convocatoria.TipoPrograma)
            .HasColumnName("tipo_programa")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(convocatoria => convocatoria.Estado)
            .HasColumnName("estado")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(convocatoria => convocatoria.InicioInscripciones)
            .HasColumnName("inicio_inscripciones")
            .IsRequired();

        builder.Property(convocatoria => convocatoria.FinInscripciones)
            .HasColumnName("fin_inscripciones")
            .IsRequired();

        builder.Property(convocatoria => convocatoria.InicioPrograma)
            .HasColumnName("inicio_programa")
            .IsRequired();

        builder.Property(convocatoria => convocatoria.FinPrograma)
            .HasColumnName("fin_programa")
            .IsRequired();

        builder.Property(convocatoria => convocatoria.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired();

        builder.Property(convocatoria => convocatoria.FechaModificacion)
            .HasColumnName("fecha_modificacion");

        builder.HasIndex(convocatoria => new
        {
            convocatoria.TipoPrograma,
            convocatoria.Estado
        });
    }
}
