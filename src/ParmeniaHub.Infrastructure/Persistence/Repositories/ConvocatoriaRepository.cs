using Microsoft.EntityFrameworkCore;
using ParmeniaHub.Application.Convocatorias;
using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Infrastructure.Persistence.Repositories;

public sealed class ConvocatoriaRepository(ApplicationDbContext dbContext)
    : IConvocatoriaRepository
{
    public async Task AgregarAsync(
        Convocatoria convocatoria,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Convocatorias.AddAsync(
            convocatoria,
            cancellationToken);
    }

    public async Task<Convocatoria?> ObtenerPorIdAsync(
        Guid id,
        bool conSeguimiento = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Convocatoria> query = dbContext.Convocatorias;

        if (!conSeguimiento)
            query = query.AsNoTracking();

        return await query.SingleOrDefaultAsync(
            convocatoria => convocatoria.Id == id,
            cancellationToken);
    }

    public async Task<IReadOnlyList<Convocatoria>> ListarAsync(
        CancellationToken cancellationToken = default) =>
        await dbContext.Convocatorias
            .AsNoTracking()
            .OrderByDescending(convocatoria => convocatoria.FechaCreacion)
            .ToListAsync(cancellationToken);

    public async Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
