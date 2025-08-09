using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation
{
    public class PostService(IPostRepository postRepository) : IPostService
    {
        public async Task<IEnumerable<Post>> PegarTodosAsync(Guid sellerId)
        {
            return await postRepository.PegarTodosAsync(sellerId);
        }
        public async Task<Post> PegarPorIdAsync(Guid id)
        {
            return await postRepository.PegarPorIdAsync(id);
        }

        public async Task AdicionaAsync(Post post)
        {
            await postRepository.AdicionaAsync(post);
        }

        public async Task AlteraAsync(Post post, Guid sellerId)
        {
            var existingPost = await postRepository.PegarPorIdAsync(post.Id);

            if (existingPost == null)
            {
                throw new ArgumentException("Post not found.");
            }

            if (existingPost.VendedorId != sellerId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this post.");
            }

            await postRepository.AlteraAsync(post);
        }

        public async Task ExcluiAsync(Guid id, Guid sellerId)
        {
            var post = await postRepository.PegarPorIdAsync(id);

            if (post == null)
            {
                throw new ArgumentException("Post not found.");
            }

            if (post.VendedorId != sellerId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this post.");
            }

            await postRepository.ExcluiAsync(id);
        }
    }
}