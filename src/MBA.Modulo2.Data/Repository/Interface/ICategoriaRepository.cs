using MBA.Modulo2.Data.Models;

namespace MBA.Modulo2.Data.Interface
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria> PegarPorIdComProdutoAsync(Guid id);
    }
}