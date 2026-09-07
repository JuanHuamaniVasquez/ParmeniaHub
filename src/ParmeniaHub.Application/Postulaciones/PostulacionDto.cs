using ParmeniaHub.Domain.Convocatorias;
using ParmeniaHub.Domain.Postulaciones;

namespace ParmeniaHub.Application.Postulaciones;

public sealed record PostulacionDto(Guid Id, string NombreProyecto,
    string NombrePostulante, string Correo, TipoPrograma EtapaDeseada,
    EstadoPostulacion Estado, string Observaciones, DateTimeOffset FechaCreacion,
    DateTimeOffset? FechaModificacion, IReadOnlyList<CambioEstadoPostulacionDto> Historial);

public sealed record CambioEstadoPostulacionDto(EstadoPostulacion Estado,
    string Observaciones, DateTimeOffset Fecha);

public sealed record CrearPostulacionRequest(string NombreProyecto,
    string NombrePostulante, string Correo, TipoPrograma EtapaDeseada);

public interface IPostulacionRepository
{
    Task AgregarAsync(Postulacion postulacion, CancellationToken cancellationToken = default);
    Task<Postulacion?> ObtenerAsync(Guid id, bool conSeguimiento = false,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Postulacion>> ListarAsync(CancellationToken cancellationToken = default);
    Task GuardarCambiosAsync(CancellationToken cancellationToken = default);
}

internal static class PostulacionMapping
{
    public static PostulacionDto ToDto(this Postulacion p) => new(p.Id,
        p.NombreProyecto, p.NombrePostulante, p.Correo, p.EtapaDeseada,
        p.Estado, p.Observaciones, p.FechaCreacion, p.FechaModificacion,
        p.Historial.OrderByDescending(h => h.Fecha)
            .Select(h => new CambioEstadoPostulacionDto(h.Estado, h.Observaciones, h.Fecha)).ToList());
}
