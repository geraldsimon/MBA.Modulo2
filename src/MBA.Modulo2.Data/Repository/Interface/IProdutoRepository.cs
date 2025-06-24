using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Interface
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<List<Produto>> GetAllWithCategoryBySellerAsync(Guid Id);
        Task<List<Produto>> GetAllByCategory(Guid id);
        Task<List<Produto>> GetAllWithCategoryAsync();
        Task<Produto> GetByIdAsync(Guid? id);
        Task<Produto> GetByIdAsync(Guid? id, Guid? sellerId);
        Task<bool> GetAnyAsync(Guid id);
    }
}