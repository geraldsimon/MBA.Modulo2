using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllWithCategoryBySellerAsync(Guid Id);
        Task<List<Product>> GetAllByCategory(Guid id);
        Task<List<Product>> GetAllWithCategoryAsync();
        Task<Product> GetByIdAsync(Guid? id);
        Task<Product> GetByIdAsync(Guid? id, Guid? sellerId);
        Task<bool> GetAnyAsync(Guid id);
    }
}