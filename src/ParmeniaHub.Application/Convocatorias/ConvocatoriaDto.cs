using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Application.Convocatorias;

public sealed record ConvocatoriaDto(
    Guid Id,
    string Titulo,
    string Descripcion,
    string Requisitos,
    TipoPrograma TipoPrograma,
    DateTimeOffset InicioInscripciones,
    DateTimeOffset FinInscripciones,
    DateTimeOffset InicioPrograma,
    DateTimeOffset FinPrograma,
    EstadoConvocatoria Estado,
    bool AceptaPostulaciones);
