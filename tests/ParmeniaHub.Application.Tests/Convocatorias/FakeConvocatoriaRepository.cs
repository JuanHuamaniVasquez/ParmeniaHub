using ParmeniaHub.Application.Convocatorias;
using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Application.Tests.Convocatorias;

internal sealed class FakeConvocatoriaRepository : IConvocatoriaRepository
{
    private readonly List<Convocatoria> _convocatorias = [];

    public int VecesGuardado { get; private set; }

    public Task AgregarAsync(
        Convocatoria convocatoria,
        CancellationToken cancellationToken = default)
    {
        _convocatorias.Add(convocatoria);
        return Task.CompletedTask;
    }

    public Task<Convocatoria?> ObtenerPorIdAsync(
        Guid id,
        bool conSeguimiento = false,
        CancellationToken cancellationToken = default) =>
        Task.FromResult(_convocatorias.SingleOrDefault(item => item.Id == id));

    public Task<IReadOnlyList<Convocatoria>> ListarAsync(
        CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<Convocatoria>>(_convocatorias.ToList());

    public Task GuardarCambiosAsync(
        CancellationToken cancellationToken = default)
    {
        VecesGuardado++;
        return Task.CompletedTask;
    }

    public void AgregarExistente(Convocatoria convocatoria) =>
        _convocatorias.Add(convocatoria);
}
