using ParmeniaHub.Domain.Common;
using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Domain.Postulaciones;

public sealed class Postulacion
{
    private readonly List<CambioEstadoPostulacion> _historial = [];
    private Postulacion() { }

    private Postulacion(Guid id, string nombreProyecto, string nombrePostulante,
        string correo, TipoPrograma etapaDeseada)
    {
        Id = id;
        NombreProyecto = nombreProyecto;
        NombrePostulante = nombrePostulante;
        Correo = correo;
        EtapaDeseada = etapaDeseada;
        Estado = EstadoPostulacion.Inscripcion;
        FechaCreacion = DateTimeOffset.UtcNow;
        _historial.Add(CambioEstadoPostulacion.Crear(id, Estado, "Postulación registrada."));
    }

    public Guid Id { get; private set; }
    public string NombreProyecto { get; private set; } = string.Empty;
    public string NombrePostulante { get; private set; } = string.Empty;
    public string Correo { get; private set; } = string.Empty;
    public TipoPrograma EtapaDeseada { get; private set; }
    public EstadoPostulacion Estado { get; private set; }
    public string Observaciones { get; private set; } = string.Empty;
    public DateTimeOffset FechaCreacion { get; private set; }
    public DateTimeOffset? FechaModificacion { get; private set; }
    public IReadOnlyCollection<CambioEstadoPostulacion> Historial => _historial.AsReadOnly();

    public static Postulacion Crear(string nombreProyecto, string nombrePostulante,
        string correo, TipoPrograma etapaDeseada)
    {
        if (string.IsNullOrWhiteSpace(nombreProyecto))
            throw new DomainException("El nombre del proyecto es obligatorio.");
        if (string.IsNullOrWhiteSpace(nombrePostulante))
            throw new DomainException("El nombre del postulante es obligatorio.");
        if (string.IsNullOrWhiteSpace(correo) || !correo.Contains('@'))
            throw new DomainException("Ingresa un correo válido.");

        return new Postulacion(Guid.NewGuid(), nombreProyecto.Trim(),
            nombrePostulante.Trim(), correo.Trim(), etapaDeseada);
    }

    public void Avanzar(string observaciones)
    {
        Estado = Estado switch
        {
            EstadoPostulacion.Inscripcion => EstadoPostulacion.PrimeraSesion,
            EstadoPostulacion.PrimeraSesion => EstadoPostulacion.Aceptada,
            EstadoPostulacion.Aceptada => EstadoPostulacion.EnProceso,
            _ => throw new DomainException("La postulación ya se encuentra en proceso.")
        };
        Observaciones = observaciones?.Trim() ?? string.Empty;
        FechaModificacion = DateTimeOffset.UtcNow;
        _historial.Add(CambioEstadoPostulacion.Crear(Id, Estado, Observaciones));
    }
}
