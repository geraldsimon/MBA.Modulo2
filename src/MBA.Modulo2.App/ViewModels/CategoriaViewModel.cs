using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.App.ViewModels;

public class CategoriaViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "The name field is mandatory.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "The Description field is mandatory.")]
    public string Description { get; set; }

    public ICollection<ProdutoViewModel> Products { get; set; } = [];
}
