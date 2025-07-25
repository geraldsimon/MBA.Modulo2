
namespace MBA.Modulo2.App.ViewModels;

public class ProdutoHomeViewModel
{
    public Guid Id { get; set; } 

    public string Name { get; set; } = null!;

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string Image { get; set; }

    public string Categoria { get; set; } = null!;
}
