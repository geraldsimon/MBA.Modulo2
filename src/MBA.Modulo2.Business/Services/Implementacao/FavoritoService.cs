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

        public async Task DeletarTodosProdutosFavorito(Guid idCliente)
        {
            await _favoritoRepository.DeletarTodosProdutosFavoritoAsync(idCliente);
        }

        public async Task DeleteProdutoFavorito(Guid idCliente, Guid idProduto)
        {
            await _favoritoRepository.DeletarProdutoFavorito(idCliente, idProduto);
        }

        public async Task<IEnumerable<Favorito>> GetByIdFavoritosClienteAsync(Guid idCliente)
        {
            return await _favoritoRepository.GetByIdFavoritosClienteAsync(idCliente);
        }

        public async Task<Favorito> GetByIdProdutofavorito(Guid idCliente, Guid idProduto)
        {
            return await _favoritoRepository.GetByIdProdutoFavoritoAsync(idCliente, idProduto);
        }
    }
}
