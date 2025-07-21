using MBA.Modulo2.Spa.Services.Api;
using MBA.Modulo2.Spa.Services.Autenticacao;
using MBA.Modulo2.Spa.Services.Autenticacao.Implementacao;
using MBA.Modulo2.Spa.Services.Autenticacao.Interface;
using Microsoft.AspNetCore.Components.Authorization;

namespace MBA.Modulo2.Spa.Configuration
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<CustomHttpHandler>();

            return services;
        }
    }
}