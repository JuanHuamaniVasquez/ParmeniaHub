namespace ParmeniaHub.Domain.Entregables;

public sealed class RevisionEntregable
{
    private RevisionEntregable() { }

    private RevisionEntregable(Guid entregableId, EstadoEntregable estado,
        string comentarios)
    {
        Id = Guid.NewGuid();
        EntregableId = entregableId;
        Estado = estado;
        Comentarios = comentarios;
        Fecha = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid EntregableId { get; private set; }
    public EstadoEntregable Estado { get; private set; }
    public string Comentarios { get; private set; } = string.Empty;
    public DateTimeOffset Fecha { get; private set; }

    internal static RevisionEntregable Crear(Guid entregableId,
        EstadoEntregable estado, string comentarios) =>
        new(entregableId, estado, comentarios ?? string.Empty);
}
