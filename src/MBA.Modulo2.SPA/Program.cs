using MBA.Modulo2.Spa;
using MBA.Modulo2.Spa.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
 
builder.Services.AddHttpClients();
builder.Services.AddServices();


await builder.Build().RunAsync();