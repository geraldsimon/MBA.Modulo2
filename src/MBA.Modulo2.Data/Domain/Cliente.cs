using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Domain
{
    public class Cliente
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Produto> Favoritos { get; set; } = [];
    }
}
