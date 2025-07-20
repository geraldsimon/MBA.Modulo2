using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IComentarioService
    {
        Task<IEnumerable<Comentario>> GetAllAsync(Guid sellerId);
        Task<Comentario> GetByIdAsync(Guid id);
        Task AddAsync(Comentario comment);
        Task UpdateAsync(Comentario comment, Guid sellerId);
        Task DeleteAsync(Guid id, Guid sellerId);
    }
}