using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class CategoriaService(ICategoriaRepository categoriaRepository) : ICategoryService
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _categoriaRepository.GetAllAsync();
    }

    public async Task<Categoria> GetByIdAsync(Guid id)
    {
        return await _categoriaRepository.GetByIdAsync(id);
    }

    public async Task<Categoria> GetByIdWithProdutoAsync(Guid id)
    {
        return await _categoriaRepository.GetByIdWithProdutoAsync(id);
    }

    public async Task<Categoria> GetByNameAsync(string name)
    {
        return await _categoriaRepository.GetByNameAsync(name);
    }

    public async Task AddAsync(Categoria categoria)
    {
         await _categoriaRepository.AddAsync(categoria);
    }

    public async Task UpdateAsync(Categoria categoria)
    {
        await _categoriaRepository.UpdateAsync(categoria);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _categoriaRepository.DeleteAsync(id);
    }
}