using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Interface
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<List<Produto>> PegaTodosComCategoriasPorVendedorAsync(Guid Id);
        Task<List<Produto>> PegarTodosPorCategoriaAsync(Guid id);
        Task<List<Produto>> PegaTodosComCategoriasAsync();
        Task<Produto> PegaPorIdAsync(Guid? id);
        Task<Produto> PegaPorIdAsync(Guid? id, Guid? sellerId);
        Task<bool> PegaPorIdAsync(Guid id);

        Task<Produto> DetalheProduto(Guid? id);
    }
}