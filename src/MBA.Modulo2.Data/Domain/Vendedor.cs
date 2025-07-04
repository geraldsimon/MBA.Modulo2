namespace MBA.Modulo2.Data.Models;

public class Vendedor
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Produto> Products { get; set; } = [];
}