﻿using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data.Repository.Implamentation
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context) : base(context) => _context = context;

        public async Task<List<Post>> PegarTodosAsync(Guid sellerId)
        {
            return await _context.Posts
                            .AsNoTracking()
                            .Where(p => p.VendedorId == sellerId)
                            .Include(c => c.Comentarios)
                            .ToListAsync();
        }
    }
}