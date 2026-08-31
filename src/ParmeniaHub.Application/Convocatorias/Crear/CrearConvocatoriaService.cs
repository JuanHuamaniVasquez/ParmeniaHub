using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Application.Convocatorias.Crear;

public sealed class CrearConvocatoriaService(
    IConvocatoriaRepository convocatoriaRepository)
{
    public async Task<Guid> EjecutarAsync(
        CrearConvocatoriaRequest request,
        CancellationToken cancellationToken = default)
    {
        var convocatoria = Convocatoria.Crear(
            request.Titulo,
            request.Descripcion,
            request.Requisitos,
            request.TipoPrograma,
            request.InicioInscripciones,
            request.FinInscripciones,
            request.InicioPrograma,
            request.FinPrograma);

        await convocatoriaRepository.AgregarAsync(
            convocatoria,
            cancellationToken);

        await convocatoriaRepository.GuardarCambiosAsync(cancellationToken);

        return convocatoria.Id;
    }
}
