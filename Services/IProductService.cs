using BlazorWasmTest.Models;

namespace BlazorWasmTest.Services;

public interface IProductService
{
    public Task<List<Product>> GetAllAsync();

    public Task AddAsync(Product product);

    public Task DeleteAsync(int productId);
}