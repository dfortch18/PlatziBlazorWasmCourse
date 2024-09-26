using BlazorWasmTest.Models;

namespace BlazorWasmTest.Services;

public interface ICategoryService
{
    public Task<List<Category>> GetAllAsync();
}