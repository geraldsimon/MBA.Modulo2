using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Interface;

namespace MBA.Modulo2.Data.Repository.Interface
{
    public interface IFavoritoRepository : IRepository<Favorito>
    {
        Task DeletarProdutoFavorito(Guid idCliente, Guid idProduto);

        Task<Favorito> GetByIdProdutoFavoritoAsync(Guid idCliente, Guid idProduto);

        Task<IEnumerable<Favorito>> GetByIdFavoritosClienteAsync(Guid idCliente);

        Task DeletarTodosProdutosFavoritoAsync(Guid idCliente);
    }
}
