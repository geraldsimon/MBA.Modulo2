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
        return await _context.Produtos
                        .Include(c => c.Category)
                        .Where(c => c.Active)
                        .ToListAsync();
    }

    public async Task<List<Produto>> GetAllByCategory(Guid id)
    {
        return await _context.Produtos
                        .Where(c => c.CategoryId == id)
                        .Include(c => c.Category)
                        .ToListAsync();
    }

    public async Task<List<Produto>> GetAllWithCategoryByVendedorAsync(Guid Id)
    {
        return await _context.Produtos.Where(s => s.VendedorId == Id)
                        .Include(c => c.Category)
                        .Include(s => s.Vendedor)
                        .ToListAsync();
    }

    public async Task<Produto> GetByIdAsync(Guid? id)
    {
        return await _context.Produtos
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Produto> GetByIdAsync(Guid? id, Guid? sellerId)
    {
        return await _context.Produtos
            .Where(p => p.Id == id && p.VendedorId == sellerId)
            .AsNoTracking()
            .Include(c => c.Category)
            .Include(s => s.Vendedor)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> GetAnyAsync(Guid id)
    {
        return await _context.Produtos.AnyAsync(e => e.Id == id);
    }

    public async Task<Produto> DetalheProduto(Guid? id)
    {
        var produto = await _context.Produtos
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Vendedor)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (produto != null)
        {
            // Carrega os produtos do vendedor separadamente
            produto.Vendedor.Produtos = await _context.Produtos
                .AsNoTracking()
                .Where(p => p.VendedorId == produto.VendedorId && p.Id != produto.Id)
                .ToListAsync();
        }

        return produto;
    }


}