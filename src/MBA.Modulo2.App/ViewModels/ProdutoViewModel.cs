using MBA.Modulo2.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MBA.Modulo2.App.ViewModels;

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

    public string Imagem { get; set; }
    public bool Ativo { get; set; }

    [JsonIgnore]
    public IFormFile ImagemFile { get; set; }

    public Guid CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;

    // FK for Vendedor
    public Guid VendedorId { get; set; }
    public Vendedor Vendedor { get; set; } = null!;
}
