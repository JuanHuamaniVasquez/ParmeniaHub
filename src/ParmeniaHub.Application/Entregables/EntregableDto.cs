using ParmeniaHub.Domain.Entregables;

namespace ParmeniaHub.Application.Entregables;

public sealed record EntregableDto(Guid Id, Guid PostulacionId, string Nombre,
    string Descripcion, EstadoEntregable Estado, string Comentarios,
    DateTimeOffset FechaCreacion, DateTimeOffset? FechaModificacion,
    IReadOnlyList<RevisionEntregableDto> Revisiones);

public sealed record RevisionEntregableDto(EstadoEntregable Estado,
    string Comentarios, DateTimeOffset Fecha);

public sealed record CrearEntregableRequest(Guid PostulacionId, string Nombre,
    string Descripcion);

public interface IEntregableRepository
{
    Task AgregarAsync(Entregable entregable, CancellationToken cancellationToken = default);
    Task<Entregable?> ObtenerAsync(Guid id, bool conSeguimiento = false,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entregable>> ListarAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Entregable>> ListarPorPostulacionAsync(Guid postulacionId,
        CancellationToken cancellationToken = default);
    Task GuardarCambiosAsync(CancellationToken cancellationToken = default);
}

internal static class EntregableMapping
{
    public static EntregableDto ToDto(this Entregable e) => new(e.Id,
        e.PostulacionId, e.Nombre, e.Descripcion, e.Estado, e.Comentarios,
        e.FechaCreacion, e.FechaModificacion,
        e.Revisiones.OrderByDescending(r => r.Fecha)
            .Select(r => new RevisionEntregableDto(r.Estado, r.Comentarios, r.Fecha)).ToList());
}
