using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Interface
{
    public interface IVendedorRepository : IRepository<Vendedor>
    {
        Task<Vendedor> PegarPorIdComProdutosAsync(Guid id);
    }
}