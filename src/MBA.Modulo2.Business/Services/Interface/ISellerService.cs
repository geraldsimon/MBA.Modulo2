using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface ISellerService
    {
        Task AddAsync(Vendedor seller);
    }
}
