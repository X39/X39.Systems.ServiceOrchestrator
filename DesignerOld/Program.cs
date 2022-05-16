using System.Reflection;
using Blazored.LocalStorage;
using X39.Systems.ServiceOrchestrator.Designer;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using X39.Util.Blazor;

var builder = WebAssemblyHostBuilder
    .CreateDefault(args);
builder.Services.AddAttributedSingletonsOf(typeof(Program).Assembly);
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddLogging();
builder.Services.AddLocalization((options) =>
{
    // options.ResourcesPath = "Resources";
});
builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
await builder.Build().RunAsync();