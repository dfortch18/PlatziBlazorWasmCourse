using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasmTest;
using BlazorWasmTest.Services;
using System.Text.Json;
using Blazored.Toast;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration.GetValue<string>("ApiUrl");

builder.Services.AddBlazoredToast();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl!) });

var serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

builder.Services.AddScoped(sp => serializerOptions);

builder.Services.AddScoped<ICategoryService>(sp =>
{
    var client = sp.GetRequiredService<HttpClient>();
    var options = sp.GetRequiredService<JsonSerializerOptions>();
    return new CategoryService(client, options);
});

builder.Services.AddScoped<IProductService>(sp =>
{
    var client = sp.GetRequiredService<HttpClient>();
    var options = sp.GetRequiredService<JsonSerializerOptions>();
    return new ProductService(client, options);
});

await builder.Build().RunAsync();
