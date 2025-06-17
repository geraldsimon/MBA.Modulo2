using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Product>> GetAllByCategory(Guid id)
    {
        return await _productRepository.GetAllByCategory(id);
    }

    public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
    {
        return await _productRepository.GetAllWithCategoryAsync();
    }

    public async Task<IEnumerable<Product>> GetAllWithCategoryBySellerAsync(Guid id)
    {
        return await _productRepository.GetAllWithCategoryBySellerAsync(id);
    }

    public async Task<Product> GetByIdAsync(Guid id, Guid sellerId)
    {
        return await _productRepository.GetByIdAsync(id, sellerId);
    }

    public async Task<Product> GetByIdAsync(Guid? id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<bool> GetAnyAsync(Guid id)
    {
        return await _productRepository.GetAnyAsync(id);
    }
        
    public async Task<bool> AddAsync(Product product)
    {
        await _productRepository.AddAsync(product);

        return true;
    }
  
    public async Task<bool> DeleteAsync(Guid id)
    {
        await _productRepository.DeleteAsync(id);
       return true;
    }

    public async Task UpdateAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }
}