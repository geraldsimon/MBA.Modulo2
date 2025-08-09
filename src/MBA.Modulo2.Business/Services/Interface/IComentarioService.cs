using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IComentarioService
    {
        Task<IEnumerable<Comentario>> PegarTodosAsync(Guid sellerId);
        Task<Comentario> PegarPorIdAsync(Guid id);
        Task AdicionaAsync(Comentario comment);
        Task AlteraAsync(Comentario comment, Guid sellerId);
        Task ExcluiAsync(Guid id, Guid sellerId);
    }
}