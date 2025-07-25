using System.ComponentModel.DataAnnotations.Schema;
using MBA.Modulo2.Shared.ViewModels;

namespace MBA.Modulo2.Api.ViewModels
{
    public class ProdutoDetalhesViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string Imagem { get; set; }
        public string ImagemUpload { get; set; }
        public Guid VendedorId { get; set; }
        public VendedorViewModel Vendedor { get; set; }

        public CategoriaViewModel Categoria { get; set; }
    }


}
