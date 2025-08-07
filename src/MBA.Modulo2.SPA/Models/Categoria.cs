namespace MBA.Modulo2.Spa.Models
{
    public class Categoria
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Nome { get; set; } = null!;

        public string Descricao { get; set; }
        public ICollection<Produto> Produtos { get; set; } = [];
    }
}