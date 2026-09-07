using Microsoft.AspNetCore.Mvc;
using ParmeniaHub.Application.Common.Exceptions;
using ParmeniaHub.Application.Postulaciones;
using ParmeniaHub.Application.Entregables;
using ParmeniaHub.Application.Herramientas;
using ParmeniaHub.Domain.Common;
using ParmeniaHub.Web.ViewModels.Postulaciones;

namespace ParmeniaHub.Web.Controllers;

public sealed class PostulacionesController(CrearPostulacionService crear,
    ListarPostulacionesService listar, ObtenerPostulacionService obtener,
    AvanzarPostulacionService avanzar,
    ListarEntregablesPorPostulacionService listarEntregables,
    EvaluarAvancePostulacionService evaluarAvance,
    CalcularProgresoProyectoService calcularProgreso) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct) => View(await listar.EjecutarAsync(ct));

    [HttpGet]
    public IActionResult Crear() => View(new CrearPostulacionViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(CrearPostulacionViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(model);
        try
        {
            var id = await crear.EjecutarAsync(new(model.NombreProyecto,
                model.NombrePostulante, model.Correo, model.EtapaDeseada), ct);
            TempData["SuccessMessage"] = "La postulación se registró correctamente.";
            return RedirectToAction(nameof(Detalle), new { id });
        }
        catch (DomainException ex) { ModelState.AddModelError(string.Empty, ex.Message); return View(model); }
    }

    [HttpGet]
    public async Task<IActionResult> Detalle(Guid id, CancellationToken ct)
    {
        try { return View(await ConstruirDetalle(id, ct)); }
        catch (NotFoundException) { return NotFound(); }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EvaluarAvance(Guid id, bool datosCompletos,
        bool asistioPrimeraSesion, bool ideaViable, bool encargadoAprobo,
        CancellationToken ct)
    {
        try
        {
            var model = await ConstruirDetalle(id, ct);
            model.DatosCompletos = datosCompletos;
            model.AsistioPrimeraSesion = asistioPrimeraSesion;
            model.IdeaViable = ideaViable;
            model.EncargadoAprobo = encargadoAprobo;
            model.Evaluacion = evaluarAvance.Ejecutar(new(
                model.Postulacion.Estado, datosCompletos, asistioPrimeraSesion,
                ideaViable, encargadoAprobo));
            return View("Detalle", model);
        }
        catch (NotFoundException) { return NotFound(); }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Avanzar(Guid id, string observaciones, CancellationToken ct)
    {
        try { await avanzar.EjecutarAsync(id, observaciones, ct); TempData["SuccessMessage"] = "El estado avanzó correctamente."; }
        catch (NotFoundException) { return NotFound(); }
        catch (DomainException ex) { TempData["ErrorMessage"] = ex.Message; }
        return RedirectToAction(nameof(Detalle), new { id });
    }

    private async Task<DetallePostulacionViewModel> ConstruirDetalle(Guid id,
        CancellationToken ct)
    {
        var postulacion = await obtener.EjecutarAsync(id, ct);
        var entregables = await listarEntregables.EjecutarAsync(id, ct);
        return new DetallePostulacionViewModel
        {
            Postulacion = postulacion,
            Entregables = entregables,
            Progreso = calcularProgreso.Ejecutar(entregables.Select(x => x.Estado))
        };
    }
}
