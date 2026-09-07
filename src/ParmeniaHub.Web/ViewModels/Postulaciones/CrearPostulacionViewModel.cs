using System.ComponentModel.DataAnnotations;
using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Web.ViewModels.Postulaciones;

public sealed class CrearPostulacionViewModel
{
    [Required, Display(Name = "Nombre del proyecto")]
    public string NombreProyecto { get; set; } = string.Empty;
    [Required, Display(Name = "Nombre del postulante")]
    public string NombrePostulante { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Correo { get; set; } = string.Empty;
    [Display(Name = "Etapa deseada")]
    public TipoPrograma EtapaDeseada { get; set; }
}
