namespace MBA.Modulo2.App.ViewModels
{
    public class VendedorViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<ProdutoViewModel> Products { get; set; } = [];
    }
}
