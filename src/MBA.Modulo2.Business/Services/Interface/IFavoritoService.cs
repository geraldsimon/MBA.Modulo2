using MBA.Modulo2.Data.Domain;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IFavoritoService
    {
        Task AdicionaAsync(Favorito favorito);

        Task ExcluirProdutoFavorito(Guid idCliente, Guid idProduto);

        Task<Favorito> PegarPorIdProdutoFavoritoAsync(Guid idCliente, Guid idProduto);

        Task<IEnumerable<Favorito>> PegarPorIdFavoritosClienteAsync(Guid idCliente);

        Task ExcluirTodosProdutosFavoritoAsync(Guid idCliente);

        Task<bool> ProdutoJaFavoritadoAsync(Guid idCliente, Guid idProduto);

    }
}
