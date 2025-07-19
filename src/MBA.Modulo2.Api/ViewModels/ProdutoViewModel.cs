using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MBA.Modulo2.Shared.ViewModels;

public class ProdutoViewModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "The name field is mandatory.")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "The Description field is mandatory.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The Price field is mandatory.")]
    [Range(0, double.MaxValue, ErrorMessage = "The price cannot be negative.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "The quantity cannot be negative.")]
    [Required(ErrorMessage = "The Stock field is mandatory.")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "The Imagem field is mandatory.")]
    public string Image { get; set; }

    public string ImageUpload { get; set; }

    public Guid CategoryId { get; set; }

    // FK for Seller
    public Guid VendedorId { get; set; }
}
