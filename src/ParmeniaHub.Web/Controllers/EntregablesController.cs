using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParmeniaHub.Application.Common.Exceptions;
using ParmeniaHub.Application.Entregables;
using ParmeniaHub.Application.Postulaciones;
using ParmeniaHub.Domain.Common;
using ParmeniaHub.Domain.Entregables;
using ParmeniaHub.Web.ViewModels.Entregables;

namespace ParmeniaHub.Web.Controllers;

public sealed class EntregablesController(CrearEntregableService crear,
    ListarEntregablesService listar, CambiarEstadoEntregableService cambiar,
    ListarPostulacionesService listarPostulaciones) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct) => View(await listar.EjecutarAsync(ct));

    [HttpGet]
    public async Task<IActionResult> Crear(CancellationToken ct)
    {
        await CargarPostulaciones(ct);
        return View(new CrearEntregableViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(CrearEntregableViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid) { await CargarPostulaciones(ct); return View(model); }
        try
        {
            await crear.EjecutarAsync(new(model.PostulacionId, model.Nombre, model.Descripcion), ct);
            TempData["SuccessMessage"] = "El entregable se creó correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (DomainException ex) { ModelState.AddModelError(string.Empty, ex.Message); await CargarPostulaciones(ct); return View(model); }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarEstado(Guid id, EstadoEntregable estado,
        string comentarios, CancellationToken ct)
    {
        try { await cambiar.EjecutarAsync(id, estado, comentarios, ct); TempData["SuccessMessage"] = "El entregable se actualizó."; }
        catch (NotFoundException) { return NotFound(); }
        catch (DomainException ex) { TempData["ErrorMessage"] = ex.Message; }
        return RedirectToAction(nameof(Index));
    }

    private async Task CargarPostulaciones(CancellationToken ct)
    {
        ViewBag.Postulaciones = new SelectList(await listarPostulaciones.EjecutarAsync(ct), "Id", "NombreProyecto");
    }
}
