using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Repository.Interface;

namespace MBA.Modulo2.Business.Services.Implementacao
{
    public class FavoritoService(IFavoritoRepository favoritoRepository) : IFavoritoService
    {
        private readonly IFavoritoRepository _favoritoRepository = favoritoRepository;
        public async Task AddAsync(Favorito favorito)
        {
            await _favoritoRepository.AddAsync(favorito);
        }

        public async Task<IEnumerable<Favorito>> GetAllAsync()
        {
            return await _favoritoRepository.GetAllAsync();
        }
    }
}
