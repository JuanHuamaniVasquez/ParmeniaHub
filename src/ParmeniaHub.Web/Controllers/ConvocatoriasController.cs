using Microsoft.AspNetCore.Mvc;
using ParmeniaHub.Application.Common.Exceptions;
using ParmeniaHub.Application.Convocatorias.Crear;
using ParmeniaHub.Application.Convocatorias.Listar;
using ParmeniaHub.Application.Convocatorias.Obtener;
using ParmeniaHub.Application.Convocatorias.Publicar;
using ParmeniaHub.Domain.Common;
using ParmeniaHub.Web.ViewModels.Convocatorias;

namespace ParmeniaHub.Web.Controllers;

public sealed class ConvocatoriasController(
    CrearConvocatoriaService crearService,
    ListarConvocatoriasService listarService,
    ObtenerConvocatoriaService obtenerService,
    PublicarConvocatoriaService publicarService) : Controller
{
    private static readonly TimeZoneInfo LimaTimeZone =
        TimeZoneInfo.FindSystemTimeZoneById("America/Lima");

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var convocatorias = await listarService.EjecutarAsync(cancellationToken);
        return View(convocatorias);
    }

    [HttpGet]
    public IActionResult Crear() => View(new CrearConvocatoriaViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(
        CrearConvocatoriaViewModel model,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var request = new CrearConvocatoriaRequest(
                model.Titulo,
                model.Descripcion,
                model.Requisitos,
                model.TipoPrograma,
                ToLimaOffset(model.InicioInscripciones),
                ToLimaOffset(model.FinInscripciones),
                ToLimaOffset(model.InicioPrograma),
                ToLimaOffset(model.FinPrograma));

            var id = await crearService.EjecutarAsync(
                request,
                cancellationToken);

            TempData["SuccessMessage"] = "La convocatoria se creó correctamente.";

            return RedirectToAction(nameof(Detalle), new { id });
        }
        catch (DomainException exception)
        {
            ModelState.AddModelError(string.Empty, exception.Message);
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Detalle(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var convocatoria = await obtenerService.EjecutarAsync(
                id,
                cancellationToken);

            return View(convocatoria);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Publicar(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            await publicarService.EjecutarAsync(id, cancellationToken);
            TempData["SuccessMessage"] = "La convocatoria se publicó correctamente.";
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (DomainException exception)
        {
            TempData["ErrorMessage"] = exception.Message;
        }

        return RedirectToAction(nameof(Detalle), new { id });
    }

    private static DateTimeOffset ToLimaOffset(DateTime dateTime)
    {
        var unspecifiedDateTime = DateTime.SpecifyKind(
            dateTime,
            DateTimeKind.Unspecified);

        return new DateTimeOffset(
            unspecifiedDateTime,
            LimaTimeZone.GetUtcOffset(unspecifiedDateTime));
    }
}
