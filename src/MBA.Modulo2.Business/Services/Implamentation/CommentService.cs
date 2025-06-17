using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class CommentService(ICommentRepository commentRepository) : ICommentService
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<IEnumerable<Comment>> GetAllAsync(Guid sellerId)
    {
        return await _commentRepository.GetAllAsync(sellerId);
    }

    public async Task<Comment> GetByIdAsync(Guid id)
    {
        return await _commentRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Comment comment)
    {
        await _commentRepository.AddAsync(comment);
    }

    public async Task UpdateAsync(Comment comment, Guid sellerId)
    {
        var existingPost = await _commentRepository.GetByIdAsync(comment.Id);

        if (existingPost == null)
        {
            throw new ArgumentException("Post not found.");
        }

        if (existingPost.SellerId != sellerId)
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

        if (post.SellerId != sellerId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this post.");
        }

        await _commentRepository.DeleteAsync(id);
    }
}