using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetAllAsync(Guid sellerId);
    }
}