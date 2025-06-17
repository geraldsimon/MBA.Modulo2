using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByIdWithProductAsync(Guid id);
    }
}