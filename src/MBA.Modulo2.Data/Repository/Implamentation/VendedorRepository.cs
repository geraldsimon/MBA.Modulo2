using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data.Repository.Implamentation;

 public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
{
    
    private readonly AppDbContext _context;
 

    public VendedorRepository(AppDbContext context) : base(context) => _context = context;

   

    public async Task<Vendedor> GetByByIdWithProductAsync(Guid id)
    {
        return await _context.Sellers
                        .Include(c => c.Products)
                        .FirstOrDefaultAsync(m => m.Id == id);
    }


}