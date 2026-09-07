using Microsoft.EntityFrameworkCore;
using ParmeniaHub.Application.Entregables;
using ParmeniaHub.Domain.Entregables;

namespace ParmeniaHub.Infrastructure.Persistence.Repositories;

public sealed class EntregableRepository(ApplicationDbContext dbContext) : IEntregableRepository
{
    public async Task AgregarAsync(Entregable entregable, CancellationToken cancellationToken = default) =>
        await dbContext.Entregables.AddAsync(entregable, cancellationToken);

    public async Task<Entregable?> ObtenerAsync(Guid id, bool conSeguimiento = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Entregable> query = dbContext.Entregables.Include(x => x.Revisiones);
        if (!conSeguimiento) query = query.AsNoTracking();
        return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Entregable>> ListarAsync(CancellationToken cancellationToken = default) =>
        await dbContext.Entregables.AsNoTracking().Include(x => x.Revisiones).OrderByDescending(x => x.FechaCreacion).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Entregable>> ListarPorPostulacionAsync(Guid postulacionId,
        CancellationToken cancellationToken = default) =>
        await dbContext.Entregables.AsNoTracking()
            .Include(x => x.Revisiones)
            .Where(x => x.PostulacionId == postulacionId)
            .OrderBy(x => x.FechaCreacion)
            .ToListAsync(cancellationToken);

    public async Task GuardarCambiosAsync(CancellationToken cancellationToken = default) =>
        await dbContext.SaveChangesAsync(cancellationToken);
}
