namespace MBA.Modulo2.Data.Models;

public class Seller
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Product> Products { get; set; } = [];
}