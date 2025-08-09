using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> PegarTodosAsync(Guid sellerId);
    }
}