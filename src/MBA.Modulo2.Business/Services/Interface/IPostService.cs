using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllAsync(Guid sellerId);
        Task<Post> GetByIdAsync(Guid id);
        Task AddAsync(Post post);
        Task UpdateAsync(Post post, Guid sellerId);
        Task DeleteAsync(Guid id, Guid sellerId);
    }
}