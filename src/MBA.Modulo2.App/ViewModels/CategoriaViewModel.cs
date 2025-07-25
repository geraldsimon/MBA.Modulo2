using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.App.ViewModels;

public class CategoriaViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "O nome é obrigatorio.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "A descricao é obrigatoria.")]
    public string Descricao { get; set; }

    public ICollection<ProdutoViewModel> Produtos { get; set; } = [];
}
