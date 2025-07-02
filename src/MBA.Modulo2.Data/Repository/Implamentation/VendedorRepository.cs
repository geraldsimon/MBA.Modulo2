using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Repository.Implamentation;

 public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
{
    public VendedorRepository(AppDbContext context) : base(context)
    {
    }
}