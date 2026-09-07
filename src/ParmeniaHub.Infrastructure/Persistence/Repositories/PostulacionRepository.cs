using Microsoft.EntityFrameworkCore;
using ParmeniaHub.Application.Postulaciones;
using ParmeniaHub.Domain.Postulaciones;

namespace ParmeniaHub.Infrastructure.Persistence.Repositories;

public sealed class PostulacionRepository(ApplicationDbContext dbContext) : IPostulacionRepository
{
    public async Task AgregarAsync(Postulacion postulacion, CancellationToken cancellationToken = default) =>
        await dbContext.Postulaciones.AddAsync(postulacion, cancellationToken);

    public async Task<Postulacion?> ObtenerAsync(Guid id, bool conSeguimiento = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Postulacion> query = dbContext.Postulaciones.Include(x => x.Historial);
        if (!conSeguimiento) query = query.AsNoTracking();
        return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Postulacion>> ListarAsync(CancellationToken cancellationToken = default) =>
        await dbContext.Postulaciones.AsNoTracking().Include(x => x.Historial).OrderByDescending(x => x.FechaCreacion).ToListAsync(cancellationToken);

    public async Task GuardarCambiosAsync(CancellationToken cancellationToken = default) =>
        await dbContext.SaveChangesAsync(cancellationToken);
}
