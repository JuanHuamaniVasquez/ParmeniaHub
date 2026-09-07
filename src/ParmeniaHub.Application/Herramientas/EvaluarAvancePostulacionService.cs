using ParmeniaHub.Domain.Postulaciones;

namespace ParmeniaHub.Application.Herramientas;

public sealed record EvaluarAvancePostulacionRequest(
    EstadoPostulacion EstadoActual,
    bool DatosCompletos,
    bool AsistioPrimeraSesion,
    bool IdeaViable,
    bool EncargadoAprobo);

public sealed record EvaluarAvancePostulacionResult(
    bool PuedeAvanzar,
    EstadoPostulacion? SiguienteEstado,
    string Mensaje,
    IReadOnlyList<string> RequisitosPendientes);

public sealed class EvaluarAvancePostulacionService
{
    public EvaluarAvancePostulacionResult Ejecutar(
        EvaluarAvancePostulacionRequest request)
    {
        if (request.EstadoActual == EstadoPostulacion.EnProceso)
        {
            return new(false, null, "La postulación ya llegó al estado final.", []);
        }

        var pendientes = new List<string>();

        if (!request.DatosCompletos)
            pendientes.Add("Completar los datos de la postulación.");

        if (request.EstadoActual == EstadoPostulacion.PrimeraSesion)
        {
            if (!request.AsistioPrimeraSesion)
                pendientes.Add("Confirmar la asistencia a la primera sesión.");
            if (!request.IdeaViable)
                pendientes.Add("Confirmar que la idea es viable.");
            if (!request.EncargadoAprobo)
                pendientes.Add("Registrar la aprobación del encargado.");
        }

        var siguiente = request.EstadoActual switch
        {
            EstadoPostulacion.Inscripcion => EstadoPostulacion.PrimeraSesion,
            EstadoPostulacion.PrimeraSesion => EstadoPostulacion.Aceptada,
            EstadoPostulacion.Aceptada => EstadoPostulacion.EnProceso,
            _ => (EstadoPostulacion?)null
        };

        return pendientes.Count == 0
            ? new(true, siguiente,
                $"La postulación está lista para avanzar a {Formatear(siguiente)}.", [])
            : new(false, siguiente,
                "La postulación todavía no está lista para avanzar.", pendientes);
    }

    private static string Formatear(EstadoPostulacion? estado) => estado switch
    {
        EstadoPostulacion.PrimeraSesion => "Primera sesión",
        EstadoPostulacion.EnProceso => "En proceso",
        _ => estado?.ToString() ?? string.Empty
    };
}
