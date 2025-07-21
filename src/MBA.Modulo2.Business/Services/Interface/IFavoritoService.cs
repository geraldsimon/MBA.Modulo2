using MBA.Modulo2.Data.Domain;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IFavoritoService
    {
        Task AddAsync(Favorito favorito);

        Task DeleteProdutoFavorito(Guid idCliente, Guid idProduto);

        Task<Favorito> GetByIdProdutofavorito(Guid idCliente, Guid idProduto);

        Task<IEnumerable<Favorito>> GetByIdFavoritosClienteAsync(Guid idCliente);

        Task DeletarTodosProdutosFavorito(Guid idCliente);

    }
}
