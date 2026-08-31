using ParmeniaHub.Domain.Common;

namespace ParmeniaHub.Domain.Convocatorias;

public sealed class Convocatoria
{
    private Convocatoria()
    {
    }

    private Convocatoria(
        Guid id,
        string titulo,
        string descripcion,
        string requisitos,
        TipoPrograma tipoPrograma,
        DateTimeOffset inicioInscripciones,
        DateTimeOffset finInscripciones,
        DateTimeOffset inicioPrograma,
        DateTimeOffset finPrograma)
    {
        Id = id;
        Titulo = titulo;
        Descripcion = descripcion;
        Requisitos = requisitos;
        TipoPrograma = tipoPrograma;
        InicioInscripciones = inicioInscripciones.ToUniversalTime();
        FinInscripciones = finInscripciones.ToUniversalTime();
        InicioPrograma = inicioPrograma.ToUniversalTime();
        FinPrograma = finPrograma.ToUniversalTime();
        Estado = EstadoConvocatoria.Borrador;
        FechaCreacion = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;
    public string Requisitos { get; private set; } = string.Empty;
    public TipoPrograma TipoPrograma { get; private set; }
    public DateTimeOffset InicioInscripciones { get; private set; }
    public DateTimeOffset FinInscripciones { get; private set; }
    public DateTimeOffset InicioPrograma { get; private set; }
    public DateTimeOffset FinPrograma { get; private set; }
    public EstadoConvocatoria Estado { get; private set; }
    public DateTimeOffset FechaCreacion { get; private set; }
    public DateTimeOffset? FechaModificacion { get; private set; }

    public static Convocatoria Crear(
        string titulo,
        string descripcion,
        string requisitos,
        TipoPrograma tipoPrograma,
        DateTimeOffset inicioInscripciones,
        DateTimeOffset finInscripciones,
        DateTimeOffset inicioPrograma,
        DateTimeOffset finPrograma)
    {
        ValidarDatos(
            titulo,
            descripcion,
            requisitos,
            inicioInscripciones,
            finInscripciones,
            inicioPrograma,
            finPrograma);

        return new Convocatoria(
            Guid.NewGuid(),
            titulo.Trim(),
            descripcion.Trim(),
            requisitos.Trim(),
            tipoPrograma,
            inicioInscripciones,
            finInscripciones,
            inicioPrograma,
            finPrograma);
    }

    public void Publicar()
    {
        if (Estado != EstadoConvocatoria.Borrador)
            throw new DomainException("Solo una convocatoria en borrador puede publicarse.");

        Estado = EstadoConvocatoria.Publicada;
        FechaModificacion = DateTimeOffset.UtcNow;
    }

    public void Cerrar()
    {
        if (Estado != EstadoConvocatoria.Publicada)
            throw new DomainException("Solo una convocatoria publicada puede cerrarse.");

        Estado = EstadoConvocatoria.Cerrada;
        FechaModificacion = DateTimeOffset.UtcNow;
    }

    public bool AceptaPostulaciones(DateTimeOffset fechaActual) =>
        Estado == EstadoConvocatoria.Publicada
        && fechaActual >= InicioInscripciones
        && fechaActual <= FinInscripciones;

    private static void ValidarDatos(
        string titulo,
        string descripcion,
        string requisitos,
        DateTimeOffset inicioInscripciones,
        DateTimeOffset finInscripciones,
        DateTimeOffset inicioPrograma,
        DateTimeOffset finPrograma)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new DomainException("El título de la convocatoria es obligatorio.");

        if (string.IsNullOrWhiteSpace(descripcion))
            throw new DomainException("La descripción de la convocatoria es obligatoria.");

        if (string.IsNullOrWhiteSpace(requisitos))
            throw new DomainException("Los requisitos de la convocatoria son obligatorios.");

        if (finInscripciones <= inicioInscripciones)
            throw new DomainException("El fin de inscripciones debe ser posterior al inicio.");

        if (inicioPrograma <= finInscripciones)
            throw new DomainException("El programa debe iniciar después del cierre de inscripciones.");

        if (finPrograma <= inicioPrograma)
            throw new DomainException("El fin del programa debe ser posterior a su inicio.");
    }
}
