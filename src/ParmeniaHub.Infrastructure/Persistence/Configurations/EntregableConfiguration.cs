using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParmeniaHub.Domain.Entregables;
using ParmeniaHub.Domain.Postulaciones;

namespace ParmeniaHub.Infrastructure.Persistence.Configurations;

public sealed class EntregableConfiguration : IEntityTypeConfiguration<Entregable>
{
    public void Configure(EntityTypeBuilder<Entregable> builder)
    {
        builder.ToTable("entregables");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.PostulacionId).HasColumnName("postulacion_id");
        builder.Property(x => x.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Descripcion).HasColumnName("descripcion").HasMaxLength(1000);
        builder.Property(x => x.Estado).HasColumnName("estado").HasConversion<string>().HasMaxLength(30);
        builder.Property(x => x.Comentarios).HasColumnName("comentarios").HasMaxLength(1000);
        builder.Property(x => x.FechaCreacion).HasColumnName("fecha_creacion");
        builder.Property(x => x.FechaModificacion).HasColumnName("fecha_modificacion");
        builder.HasOne<Postulacion>().WithMany().HasForeignKey(x => x.PostulacionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Revisiones).WithOne().HasForeignKey(x => x.EntregableId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(x => x.Revisiones).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
