namespace ParmeniaHub.Domain.Postulaciones;

public sealed class CambioEstadoPostulacion
{
    private CambioEstadoPostulacion() { }

    private CambioEstadoPostulacion(Guid postulacionId, EstadoPostulacion estado,
        string observaciones)
    {
        Id = Guid.NewGuid();
        PostulacionId = postulacionId;
        Estado = estado;
        Observaciones = observaciones;
        Fecha = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid PostulacionId { get; private set; }
    public EstadoPostulacion Estado { get; private set; }
    public string Observaciones { get; private set; } = string.Empty;
    public DateTimeOffset Fecha { get; private set; }

    internal static CambioEstadoPostulacion Crear(Guid postulacionId,
        EstadoPostulacion estado, string observaciones) =>
        new(postulacionId, estado, observaciones ?? string.Empty);
}
