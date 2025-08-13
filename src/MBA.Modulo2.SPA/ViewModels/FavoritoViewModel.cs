
using MBA.Modulo2.Spa.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MBA.Modulo2.Spa.ViewModels
{
    public class FavoritoViewModel
    {
        public DateTime DataAdicao { get; set; } = DateTime.Now;

        [ForeignKey(nameof(Produto))]
        public Guid ProdutoId { get; set; }

        [ForeignKey(nameof(Cliente))]
        public Guid ClienteId { get; set; }
    }
}