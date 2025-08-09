namespace MBA.Modulo2.App.ViewModels
{
    public class VendedorViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public ICollection<ProdutoViewModel> Produtos { get; set; } = [];
    }
}
