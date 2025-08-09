using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Shared.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace MBA.Modulo2.Api.ViewModels
{
    public class FavoritoDoClienteViewModel
    {
        public ProdutoLoggedOutViewModel Produto { get; set; }


        [ForeignKey(nameof(Cliente))]
        public Guid ClienteId { get; set; }
    }
}
