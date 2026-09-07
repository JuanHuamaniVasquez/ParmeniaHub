using ParmeniaHub.Domain.Convocatorias;
using ParmeniaHub.Domain.Postulaciones;

namespace ParmeniaHub.Domain.Tests.Postulaciones;

public sealed class PostulacionTests
{
    [Fact]
    public void Crear_IniciaEnInscripcion()
    {
        var postulacion = Postulacion.Crear("EcoApp", "Ana Pérez",
            "ana@universidad.edu", TipoPrograma.Preincubacion);
        Assert.Equal(EstadoPostulacion.Inscripcion, postulacion.Estado);
        Assert.Single(postulacion.Historial);
    }

    [Fact]
    public void Avanzar_CambiaALaPrimeraSesionYGuardaObservacion()
    {
        var postulacion = Postulacion.Crear("EcoApp", "Ana Pérez",
            "ana@universidad.edu", TipoPrograma.Preincubacion);
        postulacion.Avanzar("Asistió a la charla inicial.");
        Assert.Equal(EstadoPostulacion.PrimeraSesion, postulacion.Estado);
        Assert.Equal("Asistió a la charla inicial.", postulacion.Observaciones);
        Assert.Equal(2, postulacion.Historial.Count);
    }
}
