using System.ComponentModel.DataAnnotations;

namespace ParmeniaHub.Web.ViewModels.Entregables;

public sealed class CrearEntregableViewModel
{
    [Required, Display(Name = "Postulación")]
    public Guid PostulacionId { get; set; }
    [Required]
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}
