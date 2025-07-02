using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data.Repository.Implamentation;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context) : base(context) => _context = context;

    public async Task<List<Produto>> GetAllWithCategoryAsync()
    {
        return await _context.Products
                        .Include(c => c.Category)
                        .ToListAsync();
    }

    public async Task<List<Produto>> GetAllByCategory(Guid id)
    {
        return await _context.Products
                        .Where(c => c.CategoryId == id)
                        .Include(c => c.Category)
                        .ToListAsync();
    }

    public async Task<List<Produto>> GetAllWithCategoryBySellerAsync(Guid Id)
    {
        return await _context.Products.Where(s => s.SellerId == Id)
                        .Include(c => c.Category)
                        .Include(s => s.Seller)
                        .ToListAsync();
    }

    public async Task<Produto> GetByIdAsync(Guid? id)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Produto> GetByIdAsync(Guid? id, Guid? sellerId)
    {
        return await _context.Products
            .Where(p => p.Id == id && p.SellerId == sellerId)
            .AsNoTracking()
            .Include(c => c.Category)
            .Include(s => s.Seller)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> GetAnyAsync(Guid id)
    {
        return await _context.Products.AnyAsync(e => e.Id == id);
    }
}