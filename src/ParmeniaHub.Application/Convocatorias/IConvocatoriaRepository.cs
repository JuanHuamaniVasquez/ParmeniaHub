using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Application.Convocatorias;

public interface IConvocatoriaRepository
{
    Task AgregarAsync(
        Convocatoria convocatoria,
        CancellationToken cancellationToken = default);

    Task<Convocatoria?> ObtenerPorIdAsync(
        Guid id,
        bool conSeguimiento = false,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Convocatoria>> ListarAsync(
        CancellationToken cancellationToken = default);

    Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default);
}
