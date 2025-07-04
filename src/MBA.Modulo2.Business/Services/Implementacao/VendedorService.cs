using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class VendedorService(IVendedorRepository sellerRepository) : IVendedorService
{
    private readonly IVendedorRepository _vendedorRepository = sellerRepository;

    public async Task AddAsync(Vendedor seller)
    {
        await _vendedorRepository.AddAsync(seller);
    }

    public async Task<Vendedor> GetByByIdWithProductAsync(Guid id)
    {
        return await _vendedorRepository.GetByByIdWithProductAsync(id);
    }

    public async Task<IEnumerable<Vendedor>> GetAllAsync()
    {
        return await _vendedorRepository.GetAllAsync();
    }
    
    public async Task UpdateAsync(Vendedor vendedor)
    {
        await _vendedorRepository.UpdateAsync(vendedor);
    }
}