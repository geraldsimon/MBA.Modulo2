using MBA.Modulo2.Spa.ExternalApi;
using MBA.Modulo2.Spa.Services.Api;

namespace MBA.Modulo2.Spa.Configuration
{
    public static class HttpClientExtension
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            var urlHttpClient = "https://localhost:7015";

            services.AddHttpClient<ProdutoHttpClient>(client =>
            {
                client.BaseAddress = new Uri(urlHttpClient);
                client.Timeout = TimeSpan.FromSeconds(180);
            });

            services.AddHttpClient<ClienteHttpClient>(client =>
            {
                client.BaseAddress = new Uri(urlHttpClient);
                client.Timeout = TimeSpan.FromSeconds(180);
            });

            services.AddHttpClient<FavoritoHttpClient>(client =>
            {
                client.BaseAddress = new Uri(urlHttpClient);
                client.Timeout = TimeSpan.FromSeconds(180);
            });

            services.AddHttpClient<CategoriaHttpClient>(client =>
            {
                client.BaseAddress = new Uri(urlHttpClient);
                client.Timeout = TimeSpan.FromSeconds(180);
            });

            services.AddHttpClient<AuthHttpClient>(client =>
            {
                client.BaseAddress = new Uri(urlHttpClient);
                client.Timeout = TimeSpan.FromSeconds(180);
            })
            .AddHttpMessageHandler<CustomHttpHandler>();

            return services;
        }
    }
}