using MBA.Modulo2.Data.Models;
using NSwag.Annotations;

namespace MBA.Modulo2.Data.Domain
{
    public class Favorito
    {
        [OpenApiIgnore]
        public DateTime AdicionadoEm { get; set; } = DateTime.Now;

        public Guid ClienteId { get; set; }
        
        public Cliente Cliente { get; set; }

        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }

    }
}
