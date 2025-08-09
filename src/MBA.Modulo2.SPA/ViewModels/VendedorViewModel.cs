namespace MBA.Modulo2.Spa.ViewModels
{
    public class VendedorViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public List<ProdutoLoggedOutViewModel> produtoReduzidos { get; set; }
    }
}
