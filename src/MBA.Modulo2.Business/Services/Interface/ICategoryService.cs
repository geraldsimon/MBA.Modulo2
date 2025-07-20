using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria> GetByIdAsync(Guid id);
        Task<Categoria> GetByIdWithProdutoAsync(Guid id);
        Task<Categoria> GetByNameAsync(string name);
        Task AddAsync(Categoria categoria);
        Task UpdateAsync(Categoria categoria);
        Task DeleteAsync(Guid id);
    }
}
