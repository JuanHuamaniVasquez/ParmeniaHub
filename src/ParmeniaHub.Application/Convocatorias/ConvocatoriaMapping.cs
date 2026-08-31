using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Application.Convocatorias;

internal static class ConvocatoriaMapping
{
    public static ConvocatoriaDto ToDto(
        this Convocatoria convocatoria,
        DateTimeOffset fechaActual) =>
        new(
            convocatoria.Id,
            convocatoria.Titulo,
            convocatoria.Descripcion,
            convocatoria.Requisitos,
            convocatoria.TipoPrograma,
            convocatoria.InicioInscripciones,
            convocatoria.FinInscripciones,
            convocatoria.InicioPrograma,
            convocatoria.FinPrograma,
            convocatoria.Estado,
            convocatoria.AceptaPostulaciones(fechaActual));
}
