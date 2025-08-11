using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> PegarTodosAsync();
        Task<Categoria> PegarPorIdAsync(Guid id);
        Task<Categoria> PegarPorIdComProdutoAsync(Guid id);
        Task<Categoria> PegarPorNomeAsync(string name);
        Task AdicionaAsync(Categoria categoria);
        Task AlteraAsync(Categoria categoria);
        Task ExcluiAsync(Guid id);
    }
}
