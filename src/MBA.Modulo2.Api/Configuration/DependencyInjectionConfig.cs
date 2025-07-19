using MBA.Modulo2.Business.Notifications;
using MBA.Modulo2.Business.Services.Implamentation;
using MBA.Modulo2.Business.Services.Implementacao;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Repository.Implamentation;
using MBA.Modulo2.Data.Repository.Interface;

namespace MBA.Modulo2.Api.Configuration;

public static class DependencyInjectionConfig
{
	public static IServiceCollection ResolveDependencies(this IServiceCollection services)
	{
		// Data
		services.AddScoped<AppDbContext>();
		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

		services.AddScoped<IProdutoRepository, ProdutoRepository>();
		services.AddScoped<ICategoriaRepository, CategoriaRepository>();
		services.AddScoped<IVendedorRepository, VendedorRepository>();
		services.AddScoped<IPostRepository, PostRepository>();
		services.AddScoped<IComentarioRepository, ComentarioRepository>();
		services.AddScoped<IClienteRepository, ClienteRepository>();
		services.AddScoped<IDenunciaRepository, DenunciaRepository>();

		// Business
		services.AddScoped<IProdutoService, ProdutoService>();
		services.AddScoped<ICategoryService, CategoriaService>();
		services.AddScoped<IVendedorService, VendedorService>();
		services.AddScoped<IPostService, PostService>();
		services.AddScoped<ICommentService, ComentarioService>();
		services.AddScoped<IClienteService, ClienteService>();
		services.AddScoped<IDenunciaService, DenunciaService>();

		services.AddScoped<IImageService, ImageService>();
		services.AddScoped<INotifier, Notificador>();
		services.AddScoped<IUser, AspNetUser>();

		return services;
	}
}