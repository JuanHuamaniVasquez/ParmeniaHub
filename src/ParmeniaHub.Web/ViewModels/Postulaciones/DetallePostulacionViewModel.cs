using ParmeniaHub.Application.Entregables;
using ParmeniaHub.Application.Herramientas;
using ParmeniaHub.Application.Postulaciones;

namespace ParmeniaHub.Web.ViewModels.Postulaciones;

public sealed class DetallePostulacionViewModel
{
    public required PostulacionDto Postulacion { get; init; }
    public required IReadOnlyList<EntregableDto> Entregables { get; init; }
    public required ProgresoProyectoResult Progreso { get; init; }
    public bool DatosCompletos { get; set; }
    public bool AsistioPrimeraSesion { get; set; }
    public bool IdeaViable { get; set; }
    public bool EncargadoAprobo { get; set; }
    public EvaluarAvancePostulacionResult? Evaluacion { get; set; }
}
