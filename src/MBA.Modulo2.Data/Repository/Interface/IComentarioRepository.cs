using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Interface
{
    public interface IComentarioRepository : IRepository<Comentario>
    {
        Task<List<Comentario>> PegarTodosAsync(Guid sellerId);
    }
}