using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Application.Convocatorias.Crear;

public sealed record CrearConvocatoriaRequest(
    string Titulo,
    string Descripcion,
    string Requisitos,
    TipoPrograma TipoPrograma,
    DateTimeOffset InicioInscripciones,
    DateTimeOffset FinInscripciones,
    DateTimeOffset InicioPrograma,
    DateTimeOffset FinPrograma);
