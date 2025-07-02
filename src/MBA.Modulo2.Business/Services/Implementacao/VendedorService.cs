using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class VendedorService(IVendedorRepository sellerRepository) : ISellerService
{
    private readonly IVendedorRepository _sellerRepository = sellerRepository;

    public async Task AddAsync(Vendedor seller)
    {
        await _sellerRepository.AddAsync(seller);
    }
}