using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data.Repository.Implamentation
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {

        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context) : base(context) => _context = context;


        public async Task<List<Comment>> GetAllAsync(Guid sellerId)
        {
            return await _context.Comments
                            .AsNoTracking()
                            .Where(p => p.SellerId == sellerId)
                            .ToListAsync();
        }
    }
}