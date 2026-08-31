using ParmeniaHub.Application.Common.Exceptions;

namespace ParmeniaHub.Application.Convocatorias.Publicar;

public sealed class PublicarConvocatoriaService(
    IConvocatoriaRepository convocatoriaRepository)
{
    public async Task EjecutarAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var convocatoria = await convocatoriaRepository.ObtenerPorIdAsync(
            id,
            conSeguimiento: true,
            cancellationToken);

        if (convocatoria is null)
            throw new NotFoundException(
                $"No se encontró la convocatoria con identificador '{id}'.");

        convocatoria.Publicar();

        await convocatoriaRepository.GuardarCambiosAsync(cancellationToken);
    }
}
