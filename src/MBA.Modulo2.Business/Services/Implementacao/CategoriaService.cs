using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class CategoriaService(ICategoriaRepository categoriaRepository) : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

    public async Task<IEnumerable<Categoria>> PegarTodosAsync()
    {
        return await _categoriaRepository.PegarTodosAsync();
    }

    public async Task<Categoria> PegarPorIdAsync(Guid id)
    {
        return await _categoriaRepository.PegarPorIdAsync(id);
    }

    public async Task<Categoria> PegarPorIdComProdutoAsync(Guid id)
    {
        return await _categoriaRepository.PegarPorIdComProdutoAsync(id);
    }

    public async Task<Categoria> PegarPorNomeAsync(string name)
    {
        return await _categoriaRepository.PegarPorNomeAsync(name);
    }

    public async Task AdicionaAsync(Categoria categoria)
    {
         await _categoriaRepository.AdicionaAsync(categoria);
    }

    public async Task AlteraAsync(Categoria categoria)
    {
        await _categoriaRepository.AlteraAsync(categoria);
    }

    public async Task ExcluiAsync(Guid id)
    {
        await _categoriaRepository.ExcluiAsync(id);
    }
}