using MBA.Modulo2.Data.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Api.ViewModels
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