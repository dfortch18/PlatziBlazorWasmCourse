using System.Text.Json;
using BlazorWasmTest.Models;

namespace BlazorWasmTest.Services;

public class CategoryService(HttpClient client, JsonSerializerOptions serializerOptions) : ICategoryService
{
    private readonly HttpClient _client = client;

    private readonly JsonSerializerOptions _serializerOptions = serializerOptions;

    public async Task<List<Category>> GetAllAsync()
    {
        var response = await _client.GetAsync("api/v1/categories");

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(await response.Content.ReadAsStringAsync());
        }

        var stream = await response.Content.ReadAsStreamAsync();

        var categories = await JsonSerializer.DeserializeAsync<List<Category>>(stream, _serializerOptions);

        return categories ?? [];
    }
}