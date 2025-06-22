using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class CategoriaService(ICategoriaRepository categoryRepository) : ICategoryService
{
    private readonly ICategoriaRepository _categoryRepository = categoryRepository;

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Categoria> GetByIdAsync(Guid id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task<Categoria> GetByIdWithProductAsync(Guid id)
    {
        return await _categoryRepository.GetByIdWithProductAsync(id);
    }

    public async Task<Categoria> GetByNameAsync(string name)
    {
        return await _categoryRepository.GetByNameAsync(name);
    }

    public async Task AddAsync(Categoria category)
    {
         await _categoryRepository.AddAsync(category);
    }

    public async Task UpdateAsync(Categoria category)
    {
        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _categoryRepository.DeleteAsync(id);
    }
}