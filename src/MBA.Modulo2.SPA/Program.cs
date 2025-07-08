using MBA.Modulo2.Spa;
using MBA.Modulo2.Spa.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MBA.Modulo2.Spa.Services.Autentica;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
 
builder.Services.AddHttpClients();
builder.Services.AddServices();

builder.Services.AddHttpClient("ApiSpa", options =>
{
    options.BaseAddress = new Uri("https://localhost:7015/");
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();


await builder.Build().RunAsync();