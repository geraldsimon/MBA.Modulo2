using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Repository.Interface;

namespace MBA.Modulo2.Business.Services.Implementacao
{
    public class FavoritoService(IFavoritoRepository favoritoRepository) : IFavoritoService
    {
        private readonly IFavoritoRepository _favoritoRepository = favoritoRepository;
        public async Task AdicionaAsync(Favorito favorito)
        {
            await _favoritoRepository.AdicionaAsync(favorito);
        }

        public async Task ExcluirTodosProdutosFavoritoAsync(Guid idCliente)
        {
            await _favoritoRepository.ExcluirTodosProdutosFavoritoAsync(idCliente);
        }

        public async Task ExcluirProdutoFavorito(Guid idCliente, Guid idProduto)
        {
            await _favoritoRepository.ExcluirProdutoFavorito(idCliente, idProduto);
        }

        public async Task<IEnumerable<Favorito>> PegarPorIdFavoritosClienteAsync(Guid idCliente)
        {
            return await _favoritoRepository.PegarPorIdFavoritosClienteAsync(idCliente);
        }

        public async Task<Favorito> PegarPorIdProdutoFavoritoAsync(Guid idCliente, Guid idProduto)
        {
            return await _favoritoRepository.PegarPorIdProdutoFavoritoAsync(idCliente, idProduto);
        }

        public async Task<bool> ProdutoJaFavoritadoAsync(Guid idCliente, Guid idProduto)
        {
            return await _favoritoRepository.ProdutoJaFavoritadoAsync(idCliente, idProduto);
        }

    }
}
