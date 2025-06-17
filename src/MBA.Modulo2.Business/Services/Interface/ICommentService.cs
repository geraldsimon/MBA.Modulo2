using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllAsync(Guid sellerId);
        Task<Comment> GetByIdAsync(Guid id);
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment, Guid sellerId);
        Task DeleteAsync(Guid id, Guid sellerId);
    }
}