using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParmeniaHub.Domain.Postulaciones;

namespace ParmeniaHub.Infrastructure.Persistence.Configurations;

public sealed class PostulacionConfiguration : IEntityTypeConfiguration<Postulacion>
{
    public void Configure(EntityTypeBuilder<Postulacion> builder)
    {
        builder.ToTable("postulaciones");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.NombreProyecto).HasColumnName("nombre_proyecto").HasMaxLength(150).IsRequired();
        builder.Property(x => x.NombrePostulante).HasColumnName("nombre_postulante").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Correo).HasColumnName("correo").HasMaxLength(200).IsRequired();
        builder.Property(x => x.EtapaDeseada).HasColumnName("etapa_deseada").HasConversion<string>().HasMaxLength(30);
        builder.Property(x => x.Estado).HasColumnName("estado").HasConversion<string>().HasMaxLength(30);
        builder.Property(x => x.Observaciones).HasColumnName("observaciones").HasMaxLength(1000);
        builder.Property(x => x.FechaCreacion).HasColumnName("fecha_creacion");
        builder.Property(x => x.FechaModificacion).HasColumnName("fecha_modificacion");
        builder.HasMany(x => x.Historial).WithOne().HasForeignKey(x => x.PostulacionId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(x => x.Historial).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
