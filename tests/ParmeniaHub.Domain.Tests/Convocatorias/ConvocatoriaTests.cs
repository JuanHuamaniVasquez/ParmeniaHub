using ParmeniaHub.Domain.Common;
using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Domain.Tests.Convocatorias;

public sealed class ConvocatoriaTests
{
    [Fact]
    public void Crear_ConDatosValidos_CreaConvocatoriaEnBorradorYFechasUtc()
    {
        var convocatoria = CrearConvocatoriaValida();

        Assert.NotEqual(Guid.Empty, convocatoria.Id);
        Assert.Equal(EstadoConvocatoria.Borrador, convocatoria.Estado);
        Assert.Equal(TimeSpan.Zero, convocatoria.InicioInscripciones.Offset);
        Assert.Equal(TimeSpan.Zero, convocatoria.FinPrograma.Offset);
    }

    [Fact]
    public void Crear_ConTituloVacio_LanzaExcepcionDeDominio()
    {
        var exception = Assert.Throws<DomainException>(() =>
            CrearConvocatoriaValida(titulo: "   "));

        Assert.Equal(
            "El título de la convocatoria es obligatorio.",
            exception.Message);
    }

    [Fact]
    public void Crear_ConFinDeInscripcionesAnteriorAlInicio_LanzaExcepcion()
    {
        var inicio = FechaLima(2026, 9, 15, 8);
        var fin = FechaLima(2026, 9, 1, 18);

        var exception = Assert.Throws<DomainException>(() =>
            CrearConvocatoriaValida(
                inicioInscripciones: inicio,
                finInscripciones: fin));

        Assert.Contains("fin de inscripciones", exception.Message);
    }

    [Fact]
    public void Publicar_CuandoEstaEnBorrador_CambiaEstadoAPublicada()
    {
        var convocatoria = CrearConvocatoriaValida();

        convocatoria.Publicar();

        Assert.Equal(EstadoConvocatoria.Publicada, convocatoria.Estado);
        Assert.NotNull(convocatoria.FechaModificacion);
    }

    [Fact]
    public void Publicar_CuandoYaEstaPublicada_LanzaExcepcion()
    {
        var convocatoria = CrearConvocatoriaValida();
        convocatoria.Publicar();

        var exception = Assert.Throws<DomainException>(convocatoria.Publicar);

        Assert.Contains("borrador", exception.Message);
    }

    [Fact]
    public void AceptaPostulaciones_PublicadaYDentroDelPlazo_DevuelveVerdadero()
    {
        var convocatoria = CrearConvocatoriaValida();
        convocatoria.Publicar();

        var fechaDentroDelPlazo = FechaLima(2026, 9, 10, 12);

        Assert.True(convocatoria.AceptaPostulaciones(fechaDentroDelPlazo));
    }

    [Fact]
    public void AceptaPostulaciones_EnBorrador_DevuelveFalso()
    {
        var convocatoria = CrearConvocatoriaValida();

        Assert.False(
            convocatoria.AceptaPostulaciones(FechaLima(2026, 9, 10, 12)));
    }

    private static Convocatoria CrearConvocatoriaValida(
        string titulo = "Preincubación 2026",
        DateTimeOffset? inicioInscripciones = null,
        DateTimeOffset? finInscripciones = null) =>
        Convocatoria.Crear(
            titulo,
            "Programa para validar ideas de negocio.",
            "Ser estudiante de la universidad.",
            TipoPrograma.Preincubacion,
            inicioInscripciones ?? FechaLima(2026, 9, 1, 8),
            finInscripciones ?? FechaLima(2026, 9, 15, 18),
            FechaLima(2026, 9, 20, 8),
            FechaLima(2026, 12, 20, 18));

    private static DateTimeOffset FechaLima(
        int year,
        int month,
        int day,
        int hour) =>
        new(year, month, day, hour, 0, 0, TimeSpan.FromHours(-5));
}
