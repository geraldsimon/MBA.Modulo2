using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MBA.Modulo2.Data.Models;

public class Produto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Description { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [Range(0, 999999.99)]
    public int Stock { get; set; }

    public string Image { get; set; }
    public bool Active { get; set; }

    public Guid CategoryId { get; set; }
    public Categoria Category { get; set; } = null!;

    public Guid SellerId { get; set; }
    public Vendedor Seller { get; set; } = null!;
}