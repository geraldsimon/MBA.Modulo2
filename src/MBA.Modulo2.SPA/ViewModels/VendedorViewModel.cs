namespace MBA.Modulo2.Spa.ViewModels
{
    public class VendedorViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public List<ProductLoggedOutViewModel> produtoReduzidos { get; set; }
    }
}
