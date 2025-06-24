using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data.Repository.Implamentation;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context) : base(context) => _context = context;


    public async Task<Categoria> GetByIdWithProductAsync(Guid id)
    {
        return await _context.Categories
            .AsNoTracking()
            .Include(c => c.Products)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}