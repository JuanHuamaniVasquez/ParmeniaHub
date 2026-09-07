using ParmeniaHub.Application.Common.Exceptions;
using ParmeniaHub.Domain.Entregables;

namespace ParmeniaHub.Application.Entregables;

public sealed class CrearEntregableService(IEntregableRepository repository)
{
    public async Task<Guid> EjecutarAsync(CrearEntregableRequest request,
        CancellationToken cancellationToken = default)
    {
        var entregable = Entregable.Crear(request.PostulacionId, request.Nombre, request.Descripcion);
        await repository.AgregarAsync(entregable, cancellationToken);
        await repository.GuardarCambiosAsync(cancellationToken);
        return entregable.Id;
    }
}

public sealed class ListarEntregablesService(IEntregableRepository repository)
{
    public async Task<IReadOnlyList<EntregableDto>> EjecutarAsync(
        CancellationToken cancellationToken = default) =>
        (await repository.ListarAsync(cancellationToken)).Select(e => e.ToDto()).ToList();
}

public sealed class ListarEntregablesPorPostulacionService(IEntregableRepository repository)
{
    public async Task<IReadOnlyList<EntregableDto>> EjecutarAsync(Guid postulacionId,
        CancellationToken cancellationToken = default) =>
        (await repository.ListarPorPostulacionAsync(postulacionId, cancellationToken))
            .Select(e => e.ToDto()).ToList();
}

public sealed class CambiarEstadoEntregableService(IEntregableRepository repository)
{
    public async Task EjecutarAsync(Guid id, EstadoEntregable estado, string comentarios,
        CancellationToken cancellationToken = default)
    {
        var entregable = await repository.ObtenerAsync(id, true, cancellationToken)
            ?? throw new NotFoundException("No se encontró el entregable.");
        entregable.CambiarEstado(estado, comentarios);
        await repository.GuardarCambiosAsync(cancellationToken);
    }
}
