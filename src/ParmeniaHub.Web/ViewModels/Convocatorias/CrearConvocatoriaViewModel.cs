using System.ComponentModel.DataAnnotations;
using ParmeniaHub.Domain.Convocatorias;

namespace ParmeniaHub.Web.ViewModels.Convocatorias;

public sealed class CrearConvocatoriaViewModel
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(150)]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [StringLength(2000)]
    [Display(Name = "Descripción")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los requisitos son obligatorios.")]
    [StringLength(2000)]
    public string Requisitos { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Tipo de programa")]
    public TipoPrograma TipoPrograma { get; set; }

    [Required]
    [Display(Name = "Inicio de inscripciones")]
    public DateTime InicioInscripciones { get; set; } = DateTime.Today.AddDays(1).AddHours(8);

    [Required]
    [Display(Name = "Fin de inscripciones")]
    public DateTime FinInscripciones { get; set; } = DateTime.Today.AddDays(15).AddHours(18);

    [Required]
    [Display(Name = "Inicio del programa")]
    public DateTime InicioPrograma { get; set; } = DateTime.Today.AddDays(20).AddHours(8);

    [Required]
    [Display(Name = "Fin del programa")]
    public DateTime FinPrograma { get; set; } = DateTime.Today.AddMonths(3).AddHours(18);
}
