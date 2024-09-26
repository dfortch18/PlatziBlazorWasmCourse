using System.Net.Http.Json;
using System.Text.Json;
using BlazorWasmTest.Models;

namespace BlazorWasmTest.Services;

public class ProductService(HttpClient client, JsonSerializerOptions serializerOptions) : IProductService
{

    private readonly HttpClient _client = client;

    private readonly JsonSerializerOptions _serializerOptions = serializerOptions;

    public async Task<List<Product>> GetAllAsync()
    {
        var response = await _client.GetAsync("api/v1/products");

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        var stream = await response.Content.ReadAsStreamAsync();

        var products = await JsonSerializer.DeserializeAsync<List<Product>>(stream, _serializerOptions);

        return products ?? [];
    }

    public async Task AddAsync(Product product)
    {
        var response = await _client.PostAsync("api/v1/products", JsonContent.Create(product));

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task DeleteAsync(int productId)
    {
        var response = await _client.DeleteAsync($"api/v1/products/{productId}");

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }
    }
}