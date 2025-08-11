using MBA.Modulo2.Data.Domain;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> PegarTodosAsync();
        Task<Cliente> PegarPorIdAsync(Guid id);
        Task<Cliente> PegarClintePorAspNetUserIdAsync(Guid id);
        Task<bool> ExcluiAsync(Guid id);
        Task AlteraAsync(Cliente cliente);
        Task AdicionaAsync(Cliente customer);
    }
}
