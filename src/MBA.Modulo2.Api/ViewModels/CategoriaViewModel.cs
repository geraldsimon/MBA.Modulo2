using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.Shared.ViewModels;

public class CategoriaViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "O Nome é obrigatório.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "A Description é obrigatória.")]
    public string Description { get; set; }
}
