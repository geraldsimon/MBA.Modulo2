using System.ComponentModel.DataAnnotations.Schema;
using MBA.Modulo2.Shared.ViewModels;

namespace MBA.Modulo2.Api.ViewModels
{
    public class ProdutoDetalhesViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public string ImageUpload { get; set; }
        public Guid SellerId { get; set; }
        public VendedorViewModel Seller { get; set; }

        public CategoriaViewModel Category { get; set; }
    }


}
