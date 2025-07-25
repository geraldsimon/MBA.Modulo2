using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Interface;

namespace MBA.Modulo2.Data.Repository.Interface
{
    public interface IFavoritoRepository : IRepository<Favorito>
    {
        Task ExcluirProdutoFavorito(Guid idCliente, Guid idProduto);

        Task<Favorito> PegarPorIdProdutoFavoritoAsync(Guid idCliente, Guid idProduto);

        Task<IEnumerable<Favorito>> PegarPorIdFavoritosClienteAsync(Guid idCliente);

        Task ExcluirTodosProdutosFavoritoAsync(Guid idCliente);
    }
}
