using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class ComentarioService(IComentarioRepository commentRepository) : IComentarioService
{
    private readonly IComentarioRepository _commentRepository = commentRepository;

    public async Task<IEnumerable<Comentario>> GetAllAsync(Guid sellerId)
    {
        return await _commentRepository.GetAllAsync(sellerId);
    }

    public async Task<Comentario> GetByIdAsync(Guid id)
    {
        return await _commentRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Comentario comment)
    {
        await _commentRepository.AddAsync(comment);
    }

    public async Task UpdateAsync(Comentario comment, Guid sellerId)
    {
        var existingPost = await _commentRepository.GetByIdAsync(comment.Id);

        if (existingPost == null)
        {
            throw new ArgumentException("Post not found.");
        }

        if (existingPost.VendedorId != sellerId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this post.");
        }

        await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteAsync(Guid id, Guid sellerId)
    {
        var post = await _commentRepository.GetByIdAsync(id);

        if (post == null)
        {
            throw new ArgumentException("Post not found.");
        }

        if (post.VendedorId != sellerId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this post.");
        }

        await _commentRepository.DeleteAsync(id);
    }
}