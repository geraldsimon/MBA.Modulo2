namespace MBA.Modulo2.Data.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> PegarTodosAsync();
        Task<T> PegarPorIdAsync(Guid id);
        Task<T> PegarPorAspNetUserIdAsync(Guid applicationUserID);
        Task<T> PegarPorNomeAsync(string name);
        Task AdicionaAsync(T entity);
        Task AlteraAsync(T entity);
        Task ExcluiAsync(Guid id);
    }
}