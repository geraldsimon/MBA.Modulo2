using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class VendedorService(IVendedorRepository sellerRepository) : IVendedorService
{
    private readonly IVendedorRepository _vendedorRepository = sellerRepository;

    public async Task AdicionaAsync(Vendedor seller)
    {
        await _vendedorRepository.AdicionaAsync(seller);
    }

    public async Task<Vendedor> PegarPorIdComProdutosAsync(Guid id)
    {
        return await _vendedorRepository.PegarPorIdComProdutosAsync(id);
    }

    public async Task<IEnumerable<Vendedor>> PegarTodosAsync()
    {
        return await _vendedorRepository.PegarTodosAsync();
    }
    
    public async Task AlteraAsync(Vendedor vendedor)
    {
        await _vendedorRepository.AlteraAsync(vendedor);
    }
}