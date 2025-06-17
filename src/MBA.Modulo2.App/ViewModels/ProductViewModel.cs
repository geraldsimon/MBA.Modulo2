using MBA.Modulo2.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MBA.Modulo2.App.ViewModels;

public class ProductViewModel
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

    public string Image { get; set; }


    [JsonIgnore]
    public IFormFile ImageFile { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // FK for Seller
    public Guid SellerId { get; set; }
    public Seller Seller { get; set; } = null!;
}
