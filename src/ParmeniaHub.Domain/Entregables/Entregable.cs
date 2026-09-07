using ParmeniaHub.Domain.Common;

namespace ParmeniaHub.Domain.Entregables;

public sealed class Entregable
{
    private readonly List<RevisionEntregable> _revisiones = [];
    private Entregable() { }

    private Entregable(Guid id, Guid postulacionId, string nombre, string descripcion)
    {
        Id = id;
        PostulacionId = postulacionId;
        Nombre = nombre;
        Descripcion = descripcion;
        Estado = EstadoEntregable.Pendiente;
        FechaCreacion = DateTimeOffset.UtcNow;
        _revisiones.Add(RevisionEntregable.Crear(id, Estado, "Entregable creado."));
    }

    public Guid Id { get; private set; }
    public Guid PostulacionId { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;
    public EstadoEntregable Estado { get; private set; }
    public string Comentarios { get; private set; } = string.Empty;
    public DateTimeOffset FechaCreacion { get; private set; }
    public DateTimeOffset? FechaModificacion { get; private set; }
    public IReadOnlyCollection<RevisionEntregable> Revisiones => _revisiones.AsReadOnly();

    public static Entregable Crear(Guid postulacionId, string nombre, string descripcion)
    {
        if (postulacionId == Guid.Empty)
            throw new DomainException("La postulación es obligatoria.");
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del entregable es obligatorio.");

        return new Entregable(Guid.NewGuid(), postulacionId, nombre.Trim(),
            descripcion?.Trim() ?? string.Empty);
    }

    public void CambiarEstado(EstadoEntregable nuevoEstado, string comentarios)
    {
        if (Estado == EstadoEntregable.Aprobado)
            throw new DomainException("Un entregable aprobado ya no puede modificarse.");
        if (nuevoEstado == Estado)
            throw new DomainException("Selecciona un estado diferente al actual.");

        Estado = nuevoEstado;
        Comentarios = comentarios?.Trim() ?? string.Empty;
        FechaModificacion = DateTimeOffset.UtcNow;
        _revisiones.Add(RevisionEntregable.Crear(Id, Estado, Comentarios));
    }
}
