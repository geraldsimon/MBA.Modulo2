using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class ComentarioService(IComentarioRepository commentRepository) : IComentarioService
{
    private readonly IComentarioRepository _commentRepository = commentRepository;

    public async Task<IEnumerable<Comentario>> PegarTodosAsync(Guid sellerId)
    {
        return await _commentRepository.PegarTodosAsync(sellerId);
    }

    public async Task<Comentario> PegarPorIdAsync(Guid id)
    {
        return await _commentRepository.PegarPorIdAsync(id);
    }

    public async Task AdicionaAsync(Comentario comment)
    {
        await _commentRepository.AdicionaAsync(comment);
    }

    public async Task AlteraAsync(Comentario comment, Guid sellerId)
    {
        var existingPost = await _commentRepository.PegarPorIdAsync(comment.Id);

        if (existingPost == null)
        {
            throw new ArgumentException("Post not found.");
        }

        if (existingPost.VendedorId != sellerId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this post.");
        }

        await _commentRepository.AlteraAsync(comment);
    }

    public async Task ExcluiAsync(Guid id, Guid sellerId)
    {
        var post = await _commentRepository.PegarPorIdAsync(id);

        if (post == null)
        {
            throw new ArgumentException("Post not found.");
        }

        if (post.VendedorId != sellerId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this post.");
        }

        await _commentRepository.ExcluiAsync(id);
    }
}