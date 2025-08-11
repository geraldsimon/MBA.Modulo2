
namespace MBA.Modulo2.Spa.Models
{
    public class Favorito
    {
        
        public DateTime AdicionadoEm { get; set; } = DateTime.Now;

        public Guid ClienteId { get; set; }
        
        public Cliente Cliente { get; set; }

        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }

    }
}
