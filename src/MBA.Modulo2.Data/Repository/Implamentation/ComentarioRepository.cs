using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data.Repository.Implamentation
{
    public class ComentarioRepository : Repository<Comentario>, IComentarioRepository
    {

        private readonly AppDbContext _context;

        public ComentarioRepository(AppDbContext context) : base(context) => _context = context;


        public async Task<List<Comentario>> PegarTodosAsync(Guid sellerId)
        {
            return await _context.Comentarios
                            .AsNoTracking()
                            .Where(p => p.VendedorId == sellerId)
                            .ToListAsync();
        }
    }
}