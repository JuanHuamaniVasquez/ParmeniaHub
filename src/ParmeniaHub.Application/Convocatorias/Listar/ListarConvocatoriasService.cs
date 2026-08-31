namespace ParmeniaHub.Application.Convocatorias.Listar;

public sealed class ListarConvocatoriasService(
    IConvocatoriaRepository convocatoriaRepository)
{
    public async Task<IReadOnlyList<ConvocatoriaDto>> EjecutarAsync(
        CancellationToken cancellationToken = default)
    {
        var convocatorias = await convocatoriaRepository.ListarAsync(
            cancellationToken);

        var fechaActual = DateTimeOffset.UtcNow;

        return convocatorias
            .Select(convocatoria => convocatoria.ToDto(fechaActual))
            .ToList();
    }
}
