using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class ProdutoService(IProdutoRepository productRepository) : IProdutoService
{
    private readonly IProdutoRepository _productRepository = productRepository;

    public async Task<IEnumerable<Produto>> PegarTodosAsync()
    {
        return await _productRepository.PegarTodosAsync();
    }

    public async Task<IEnumerable<Produto>> PegarTodosPorCatgoria(Guid id)
    {
        return await _productRepository.PegarTodosPorCategoriaAsync(id);
    }

    public async Task<IEnumerable<Produto>> PegarTodosComCategoriaAsync()
    {
        return await _productRepository.PegaTodosComCategoriasAsync();
    }

    public async Task<IEnumerable<Produto>> PegaTodosComCategoriasPorVendedorAsync(Guid id)
    {
        return await _productRepository.PegaTodosComCategoriasPorVendedorAsync(id);
    }

    public async Task<Produto> PegaPorIdAsync(Guid id, Guid sellerId)
    {
        return await _productRepository.PegaPorIdAsync(id, sellerId);
    }

    public async Task<Produto> PegaPorIdAsync(Guid? id)
    {
        return await _productRepository.PegaPorIdAsync(id);
    }

    public async Task<bool> PegaPorIdAsync(Guid id)
    {
        return await _productRepository.PegaPorIdAsync(id);
    }
        
    public async Task<bool> AdicionaAsync(Produto product)
    {
        await _productRepository.AdicionaAsync(product);

        return true;
    }
  
    public async Task<bool> ExcluiAsync(Guid id)
    {
        await _productRepository.ExcluiAsync(id);
       return true;
    }

    public async Task AlteraAsync(Produto product)
    {
        await _productRepository.AlteraAsync(product);
    }


    public async Task<Produto> DetalheProduto(Guid? id)
    {
        return await _productRepository.DetalheProduto(id);
    }

}