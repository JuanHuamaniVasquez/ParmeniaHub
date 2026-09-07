using ParmeniaHub.Domain.Entregables;

namespace ParmeniaHub.Application.Herramientas;

public sealed record ProgresoProyectoResult(
    decimal Porcentaje,
    string Nivel,
    string SiguienteAccion,
    int TotalEntregables,
    int EntregablesAprobados);

public sealed class CalcularProgresoProyectoService
{
    public ProgresoProyectoResult Ejecutar(IEnumerable<EstadoEntregable> estados)
    {
        var lista = estados.ToList();
        if (lista.Count == 0)
            return new(0, "Sin entregables", "Registrar el primer entregable.", 0, 0);

        var porcentaje = Math.Round(lista.Average(ValorEstado), 2);
        var nivel = porcentaje switch
        {
            <= 25 => "En inicio",
            <= 50 => "Avance bajo",
            <= 80 => "Buen progreso",
            < 100 => "Cerca de completar",
            _ => "Completado"
        };

        var siguienteAccion = lista.Contains(EstadoEntregable.RequiereCambios)
            ? "Atender los entregables que requieren cambios."
            : lista.Contains(EstadoEntregable.Pendiente)
                ? "Preparar y enviar los entregables pendientes."
                : lista.Contains(EstadoEntregable.Enviado)
                    ? "Esperar o solicitar la revisión de los entregables enviados."
                    : lista.Contains(EstadoEntregable.EnRevision)
                        ? "Esperar el resultado de los entregables en revisión."
                        : "Todos los entregables están aprobados.";

        return new(porcentaje, nivel, siguienteAccion, lista.Count,
            lista.Count(x => x == EstadoEntregable.Aprobado));
    }

    private static decimal ValorEstado(EstadoEntregable estado) => estado switch
    {
        EstadoEntregable.Pendiente => 0,
        EstadoEntregable.Enviado => 40,
        EstadoEntregable.EnRevision => 60,
        EstadoEntregable.RequiereCambios => 50,
        EstadoEntregable.Aprobado => 100,
        _ => 0
    };
}
