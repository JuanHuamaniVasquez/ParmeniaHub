using ParmeniaHub.Domain.Entregables;

namespace ParmeniaHub.Domain.Tests.Entregables;

public sealed class EntregableTests
{
    [Fact]
    public void Crear_IniciaPendiente()
    {
        var entregable = Entregable.Crear(Guid.NewGuid(), "Modelo de negocio", "Primera versión");
        Assert.Equal(EstadoEntregable.Pendiente, entregable.Estado);
        Assert.Single(entregable.Revisiones);
    }

    [Fact]
    public void CambiarEstado_GuardaEstadoYComentario()
    {
        var entregable = Entregable.Crear(Guid.NewGuid(), "Modelo de negocio", "Primera versión");
        entregable.CambiarEstado(EstadoEntregable.Enviado, "Documento adjunto.");
        Assert.Equal(EstadoEntregable.Enviado, entregable.Estado);
        Assert.Equal("Documento adjunto.", entregable.Comentarios);
        Assert.Equal(2, entregable.Revisiones.Count);
    }
}
