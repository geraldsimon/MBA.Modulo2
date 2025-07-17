namespace MBA.Modulo2.Shared.ViewModels;

public class ProductLoggedOutViewModel
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; } = null!;
    
    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }
    public string Image { get; set; }

    public string ImageUpload { get; set; }
}
