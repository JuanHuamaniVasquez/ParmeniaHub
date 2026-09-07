using ParmeniaHub.Application.Herramientas;
using ParmeniaHub.Domain.Entregables;
using ParmeniaHub.Domain.Postulaciones;

namespace ParmeniaHub.Application.Tests.Herramientas;

public sealed class HerramientasIntegradasTests
{
    [Fact]
    public void EvaluarAvance_InscripcionCompleta_PermitePrimeraSesion()
    {
        var resultado = new EvaluarAvancePostulacionService().Ejecutar(
            new(EstadoPostulacion.Inscripcion, true, false, false, false));

        Assert.True(resultado.PuedeAvanzar);
        Assert.Equal(EstadoPostulacion.PrimeraSesion, resultado.SiguienteEstado);
    }

    [Fact]
    public void EvaluarAvance_PrimeraSesionSinAprobacion_IndicaLoPendiente()
    {
        var resultado = new EvaluarAvancePostulacionService().Ejecutar(
            new(EstadoPostulacion.PrimeraSesion, true, true, true, false));

        Assert.False(resultado.PuedeAvanzar);
        Assert.Contains(resultado.RequisitosPendientes,
            requisito => requisito.Contains("aprobación"));
    }

    [Fact]
    public void CalcularProgreso_UsaLosEstadosDeLosEntregables()
    {
        var estados = new[]
        {
            EstadoEntregable.Aprobado,
            EstadoEntregable.EnRevision,
            EstadoEntregable.Pendiente
        };

        var resultado = new CalcularProgresoProyectoService().Ejecutar(estados);

        Assert.Equal(53.33m, resultado.Porcentaje);
        Assert.Equal("Buen progreso", resultado.Nivel);
        Assert.Equal(1, resultado.EntregablesAprobados);
    }

    [Fact]
    public void CalcularProgreso_SinEntregables_InvitaARegistrarUno()
    {
        var resultado = new CalcularProgresoProyectoService().Ejecutar([]);

        Assert.Equal(0, resultado.Porcentaje);
        Assert.Equal("Sin entregables", resultado.Nivel);
    }
}
