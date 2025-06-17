using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Repository.Implamentation;

 public class SellerRepository : Repository<Seller>, ISellerRepository
{
    public SellerRepository(AppDbContext context) : base(context)
    {
    }
}