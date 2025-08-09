using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> PegarTodosAsync(Guid sellerId);
        Task<Post> PegarPorIdAsync(Guid id);
        Task AdicionaAsync(Post post);
        Task AlteraAsync(Post post, Guid sellerId);
        Task ExcluiAsync(Guid id, Guid sellerId);
    }
}