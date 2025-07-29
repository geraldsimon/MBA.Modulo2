namespace MBA.Modulo2.Shared.ViewModels;

public class ProdutoLoggedOutViewModel
{
    public Guid Id { get; set; } = new Guid();
    public string Nome { get; set; } = null!;

    public string Descricao { get; set; }

    public int Estoque { get; set; }

    public int Preco { get; set; }
    public string Imagem { get; set; }

    public string ImagemUpload { get; set; }
}
