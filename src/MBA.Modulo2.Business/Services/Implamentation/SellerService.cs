using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Implamentation;

public class SellerService(ISellerRepository sellerRepository) : ISellerService
{
    private readonly ISellerRepository _sellerRepository = sellerRepository;

    public async Task AddAsync(Seller seller)
    {
        await _sellerRepository.AddAsync(seller);
    }
}