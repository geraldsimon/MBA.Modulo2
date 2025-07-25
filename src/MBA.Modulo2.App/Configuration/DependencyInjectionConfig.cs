using MBA.Modulo2.Business.Notifications;
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
        services.AddScoped<IDenunciaRepository, DenunciaRepository>();

        // Business
        services.AddScoped<IVendedorService, VendedorService>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IUser, AspNetUser>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IDenunciaService, DenunciaService>();
        services.AddScoped<INotifier, Notificador>();

        return services;
    }
}