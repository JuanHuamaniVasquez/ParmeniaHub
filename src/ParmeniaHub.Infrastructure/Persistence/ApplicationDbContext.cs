using Microsoft.EntityFrameworkCore;
using ParmeniaHub.Domain.Convocatorias;
using ParmeniaHub.Domain.Entregables;
using ParmeniaHub.Domain.Postulaciones;

namespace ParmeniaHub.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Convocatoria> Convocatorias => Set<Convocatoria>();
    public DbSet<Postulacion> Postulaciones => Set<Postulacion>();
    public DbSet<Entregable> Entregables => Set<Entregable>();
    public DbSet<CambioEstadoPostulacion> HistorialPostulaciones => Set<CambioEstadoPostulacion>();
    public DbSet<RevisionEntregable> RevisionesEntregables => Set<RevisionEntregable>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
