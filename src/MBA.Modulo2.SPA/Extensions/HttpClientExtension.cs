using MBA.Modulo2.Spa.ExternalApi;

namespace MBA.Modulo2.Spa.Extensions
{
    public static class HttpClientExtension
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            var clientRefitMBA = "https://localhost:7015";

            services.AddHttpClient<ProdutoHttpClient>(client =>
            {
                client.BaseAddress = new Uri(clientRefitMBA);
                client.Timeout = TimeSpan.FromSeconds(180);
            });

            return services;
        }
    }
}