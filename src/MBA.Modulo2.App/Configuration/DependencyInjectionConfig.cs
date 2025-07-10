using MBA.Modulo2.Business.Services.Implamentation;
using MBA.Modulo2.Business.Services.Implementacao;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Repository.Implamentation;
using MBA.Modulo2.Data.Repository.Interface;

namespace MBA.Modulo2.App.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        // Data
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IVendedorRepository, VendedorRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();

        // Business
        services.AddScoped<ISellerService, VendedorService>();
        services.AddScoped<IProductService, ProdutoService>();
        services.AddScoped<ICategoryService, CategoriaService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IUser, AspNetUser>();
        services.AddScoped<ICustomerService, ClienteService>();


        return services;
    }
}