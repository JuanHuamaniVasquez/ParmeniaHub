using ParmeniaHub.Application.Common.Exceptions;

namespace ParmeniaHub.Application.Convocatorias.Obtener;

public sealed class ObtenerConvocatoriaService(
    IConvocatoriaRepository convocatoriaRepository)
{
    public async Task<ConvocatoriaDto> EjecutarAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var convocatoria = await convocatoriaRepository.ObtenerPorIdAsync(
            id,
            cancellationToken: cancellationToken);

        if (convocatoria is null)
            throw new NotFoundException(
                $"No se encontró la convocatoria con identificador '{id}'.");

        return convocatoria.ToDto(DateTimeOffset.UtcNow);
    }
}
