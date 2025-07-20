using MBA.Modulo2.Shared.ViewModels;

namespace MBA.Modulo2.Api.ViewModels
{
    public class VendedorViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public List<ProdutoLoggedOutViewModel> produtoReduzidos { get; set; }
    }
}
