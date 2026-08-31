using ParmeniaHub.Application.Common.Exceptions;
using ParmeniaHub.Application.Convocatorias.Crear;
using ParmeniaHub.Application.Convocatorias.Listar;
using ParmeniaHub.Application.Convocatorias.Obtener;
using ParmeniaHub.Application.Convocatorias.Publicar;
using ParmeniaHub.Domain.Common;
using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Application.Tests.Convocatorias;

public sealed class ConvocatoriaServiceTests
{
    [Fact]
    public async Task Crear_ConSolicitudValida_AgregaYGuardaConvocatoria()
    {
        var repository = new FakeConvocatoriaRepository();
        var service = new CrearConvocatoriaService(repository);

        var id = await service.EjecutarAsync(CrearRequestValido());
        var convocatoria = await repository.ObtenerPorIdAsync(id);

        Assert.NotEqual(Guid.Empty, id);
        Assert.NotNull(convocatoria);
        Assert.Equal(1, repository.VecesGuardado);
        Assert.Equal(EstadoConvocatoria.Borrador, convocatoria.Estado);
    }

    [Fact]
    public async Task Crear_ConFechasInvalidas_NoAgregaNiGuarda()
    {
        var repository = new FakeConvocatoriaRepository();
        var service = new CrearConvocatoriaService(repository);
        var request = CrearRequestValido() with
        {
            FinInscripciones = FechaLima(2026, 8, 30, 18)
        };

        await Assert.ThrowsAsync<DomainException>(() =>
            service.EjecutarAsync(request));

        Assert.Empty(await repository.ListarAsync());
        Assert.Equal(0, repository.VecesGuardado);
    }

    [Fact]
    public async Task Obtener_ConIdInexistente_LanzaNotFoundException()
    {
        var service = new ObtenerConvocatoriaService(
            new FakeConvocatoriaRepository());

        await Assert.ThrowsAsync<NotFoundException>(() =>
            service.EjecutarAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task Listar_ConConvocatoriaRegistrada_DevuelveDto()
    {
        var repository = new FakeConvocatoriaRepository();
        var convocatoria = CrearConvocatoria();
        repository.AgregarExistente(convocatoria);
        var service = new ListarConvocatoriasService(repository);

        var resultado = await service.EjecutarAsync();

        var dto = Assert.Single(resultado);
        Assert.Equal(convocatoria.Id, dto.Id);
        Assert.Equal(convocatoria.Titulo, dto.Titulo);
    }

    [Fact]
    public async Task Publicar_ConvocatoriaEnBorrador_CambiaEstadoYGuarda()
    {
        var repository = new FakeConvocatoriaRepository();
        var convocatoria = CrearConvocatoria();
        repository.AgregarExistente(convocatoria);
        var service = new PublicarConvocatoriaService(repository);

        await service.EjecutarAsync(convocatoria.Id);

        Assert.Equal(EstadoConvocatoria.Publicada, convocatoria.Estado);
        Assert.Equal(1, repository.VecesGuardado);
    }

    [Fact]
    public async Task Publicar_ConvocatoriaInexistente_LanzaNotFoundYNoGuarda()
    {
        var repository = new FakeConvocatoriaRepository();
        var service = new PublicarConvocatoriaService(repository);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            service.EjecutarAsync(Guid.NewGuid()));

        Assert.Equal(0, repository.VecesGuardado);
    }

    private static CrearConvocatoriaRequest CrearRequestValido() =>
        new(
            "Preincubación 2026",
            "Programa para validar ideas de negocio.",
            "Ser estudiante de la universidad.",
            TipoPrograma.Preincubacion,
            FechaLima(2026, 9, 1, 8),
            FechaLima(2026, 9, 15, 18),
            FechaLima(2026, 9, 20, 8),
            FechaLima(2026, 12, 20, 18));

    private static Convocatoria CrearConvocatoria()
    {
        var request = CrearRequestValido();

        return Convocatoria.Crear(
            request.Titulo,
            request.Descripcion,
            request.Requisitos,
            request.TipoPrograma,
            request.InicioInscripciones,
            request.FinInscripciones,
            request.InicioPrograma,
            request.FinPrograma);
    }

    private static DateTimeOffset FechaLima(
        int year,
        int month,
        int day,
        int hour) =>
        new(year, month, day, hour, 0, 0, TimeSpan.FromHours(-5));
}
