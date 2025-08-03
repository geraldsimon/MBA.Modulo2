using System.ComponentModel.DataAnnotations.Schema;

namespace MBA.Modulo2.Spa.ViewModels
{
    public class FavoritoDoClienteViewModel
    {
        public ProdutoLoggedOutViewModel Produto { get; set; }
        public Guid ClienteId { get; set; }
        public bool success { get; set; }
    }
}
