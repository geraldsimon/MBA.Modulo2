using MBA.Modulo2.Data;
using MBA.Modulo2.Data.Domain;
using Microsoft.AspNetCore.Identity;

namespace MBA.Modulo2.App.Configuration;

public static class IdentityConfiguration
{
    public static IServiceCollection AddDbConfiguration(this IServiceCollection services, ConfigurationManager Configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}