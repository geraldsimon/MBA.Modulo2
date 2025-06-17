using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category> GetByIdAsync(Guid id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task<Category> GetByIdWithProductAsync(Guid id)
    {
        return await _categoryRepository.GetByIdWithProductAsync(id);
    }

    public async Task<Category> GetByNameAsync(string name)
    {
        return await _categoryRepository.GetByNameAsync(name);
    }

    public async Task AddAsync(Category category)
    {
         await _categoryRepository.AddAsync(category);
    }

    public async Task UpdateAsync(Category category)
    {
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _categoryRepository.DeleteAsync(id);
    }
}