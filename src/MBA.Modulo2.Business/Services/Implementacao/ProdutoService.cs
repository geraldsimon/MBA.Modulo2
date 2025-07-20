using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class ProdutoService(IProdutoRepository productRepository) : IProdutoService
{
    private readonly IProdutoRepository _productRepository = productRepository;

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Produto>> GetAllByCategory(Guid id)
    {
        return await _productRepository.GetAllByCategory(id);
    }

    public async Task<IEnumerable<Produto>> GetAllWithCategoryAsync()
    {
        return await _productRepository.GetAllWithCategoryAsync();
    }

    public async Task<IEnumerable<Produto>> GetAllWithCategoryByVendedorAsync(Guid id)
    {
        return await _productRepository.GetAllWithCategoryByVendedorAsync(id);
    }

    public async Task<Produto> GetByIdAsync(Guid id, Guid sellerId)
    {
        return await _productRepository.GetByIdAsync(id, sellerId);
    }

    public async Task<Produto> GetByIdAsync(Guid? id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<bool> GetAnyAsync(Guid id)
    {
        return await _productRepository.GetAnyAsync(id);
    }
        
    public async Task<bool> AddAsync(Produto product)
    {
        await _productRepository.AddAsync(product);

        return true;
    }
  
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _productRepository.DeleteAsync(id);
       return true;
    }

    public async Task UpdateAsync(Produto product)
    {
        await _productRepository.UpdateAsync(product);
    }


    public async Task<Produto> DetalheProduto(Guid? id)
    {
        return await _productRepository.DetalheProduto(id);
    }

}