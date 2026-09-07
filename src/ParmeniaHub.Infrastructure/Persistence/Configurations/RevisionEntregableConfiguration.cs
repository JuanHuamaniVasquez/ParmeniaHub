using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParmeniaHub.Domain.Entregables;

namespace ParmeniaHub.Infrastructure.Persistence.Configurations;

public sealed class RevisionEntregableConfiguration : IEntityTypeConfiguration<RevisionEntregable>
{
    public void Configure(EntityTypeBuilder<RevisionEntregable> builder)
    {
        builder.ToTable("revisiones_entregables");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedNever();
        builder.Property(x => x.EntregableId).HasColumnName("entregable_id");
        builder.Property(x => x.Estado).HasColumnName("estado").HasConversion<string>().HasMaxLength(30);
        builder.Property(x => x.Comentarios).HasColumnName("comentarios").HasMaxLength(1000);
        builder.Property(x => x.Fecha).HasColumnName("fecha");
    }
}
