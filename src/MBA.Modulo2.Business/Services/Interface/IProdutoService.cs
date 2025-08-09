using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> PegarTodosAsync();
        Task<IEnumerable<Produto>> PegarTodosPorCatgoria(Guid id);
        Task<IEnumerable<Produto>> PegarTodosComCategoriaAsync();
        Task<IEnumerable<Produto>> PegaTodosComCategoriasPorVendedorAsync(Guid id);
        Task<Produto> PegaPorIdAsync(Guid id, Guid sellerId);
        Task<bool> PegaPorIdAsync(Guid id);
        Task<Produto> PegaPorIdAsync(Guid? id);
        Task<Produto> DetalheProduto(Guid? id);
        Task<bool> AdicionaAsync(Produto product);
        Task<bool> ExcluiAsync(Guid id);
        Task AlteraAsync(Produto product);
    }
}