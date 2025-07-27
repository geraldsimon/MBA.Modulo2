using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IVendedorService
    {
        Task AdicionaAsync(Vendedor seller);
        Task<Vendedor> PegarPorIdComProdutosAsync(Guid id);
        Task<Vendedor> PegarVendedorPorAspNetUserIdAsync(Guid id);
        Task<IEnumerable<Vendedor>> PegarTodosAsync();
        Task AlteraAsync(Vendedor vendedor);
    }
}
