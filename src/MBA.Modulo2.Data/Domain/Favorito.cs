using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Domain
{
    public class Favorito
    {
        public DateTime DataAdicao { get; set; } = DateTime.Now;

        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
