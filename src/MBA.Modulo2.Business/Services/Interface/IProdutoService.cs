using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<IEnumerable<Produto>> GetAllByCategory(Guid id);
        Task<IEnumerable<Produto>> GetAllWithCategoryAsync();
        Task<IEnumerable<Produto>> GetAllWithCategoryByVendedorAsync(Guid id);
        Task<Produto> GetByIdAsync(Guid id, Guid sellerId);
        Task<Produto> GetByIdAsync(Guid? id);
        Task<Produto> DetalheProduto(Guid? id);
        Task<bool> GetAnyAsync(Guid id);
        Task<bool> AddAsync(Produto product);
        Task<bool> DeleteAsync(Guid id);
        Task UpdateAsync(Produto product);
    }
}