namespace MBA.Modulo2.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = null!;

        public string Description { get; set; }
        public ICollection<Product> Products { get; set; } = [];
    }
}