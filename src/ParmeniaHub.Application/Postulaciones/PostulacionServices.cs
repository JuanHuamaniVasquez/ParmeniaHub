using ParmeniaHub.Application.Common.Exceptions;
using ParmeniaHub.Domain.Postulaciones;

namespace ParmeniaHub.Application.Postulaciones;

public sealed class CrearPostulacionService(IPostulacionRepository repository)
{
    public async Task<Guid> EjecutarAsync(CrearPostulacionRequest request,
        CancellationToken cancellationToken = default)
    {
        var postulacion = Postulacion.Crear(request.NombreProyecto,
            request.NombrePostulante, request.Correo, request.EtapaDeseada);
        await repository.AgregarAsync(postulacion, cancellationToken);
        await repository.GuardarCambiosAsync(cancellationToken);
        return postulacion.Id;
    }
}

public sealed class ListarPostulacionesService(IPostulacionRepository repository)
{
    public async Task<IReadOnlyList<PostulacionDto>> EjecutarAsync(
        CancellationToken cancellationToken = default) =>
        (await repository.ListarAsync(cancellationToken)).Select(p => p.ToDto()).ToList();
}

public sealed class ObtenerPostulacionService(IPostulacionRepository repository)
{
    public async Task<PostulacionDto> EjecutarAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var postulacion = await repository.ObtenerAsync(id, cancellationToken: cancellationToken)
            ?? throw new NotFoundException("No se encontró la postulación.");
        return postulacion.ToDto();
    }
}

public sealed class AvanzarPostulacionService(IPostulacionRepository repository)
{
    public async Task EjecutarAsync(Guid id, string observaciones,
        CancellationToken cancellationToken = default)
    {
        var postulacion = await repository.ObtenerAsync(id, true, cancellationToken)
            ?? throw new NotFoundException("No se encontró la postulación.");
        postulacion.Avanzar(observaciones);
        await repository.GuardarCambiosAsync(cancellationToken);
    }
}
