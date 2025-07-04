using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IVendedorService
    {
        Task AddAsync(Vendedor seller);
        Task<Vendedor> GetByByIdWithProductAsync(Guid id);
        Task<IEnumerable<Vendedor>> GetAllAsync();
        Task UpdateAsync(Vendedor vendedor);
    }
}
