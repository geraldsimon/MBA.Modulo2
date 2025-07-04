using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Interface
{
    public interface IVendedorRepository : IRepository<Vendedor>
    {
        Task<Vendedor> GetByByIdWithProductAsync(Guid id);
    }
}