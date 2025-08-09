using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MBA.Modulo2.Shared.ViewModels;

public class ProdutoViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "O Nome é obrigatório.")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "A Descricao é obrigatória.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O Estoque é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Preco { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
    [Required(ErrorMessage = "O Estoque é obrigatório")]
    public int Estoque { get; set; }

    [Required(ErrorMessage = "The Imagem field is mandatory.")]
    public string Imagem { get; set; }

    public string ImagemUpload { get; set; }

    public Guid CategoriaId { get; set; }

    // FK for Seller
    public Guid VendedorId { get; set; }
}
