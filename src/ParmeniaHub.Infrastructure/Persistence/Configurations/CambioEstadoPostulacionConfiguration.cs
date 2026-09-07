using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParmeniaHub.Domain.Postulaciones;

namespace ParmeniaHub.Infrastructure.Persistence.Configurations;

public sealed class CambioEstadoPostulacionConfiguration : IEntityTypeConfiguration<CambioEstadoPostulacion>
{
    public void Configure(EntityTypeBuilder<CambioEstadoPostulacion> builder)
    {
        builder.ToTable("historial_postulaciones");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedNever();
        builder.Property(x => x.PostulacionId).HasColumnName("postulacion_id");
        builder.Property(x => x.Estado).HasColumnName("estado").HasConversion<string>().HasMaxLength(30);
        builder.Property(x => x.Observaciones).HasColumnName("observaciones").HasMaxLength(1000);
        builder.Property(x => x.Fecha).HasColumnName("fecha");
    }
}
