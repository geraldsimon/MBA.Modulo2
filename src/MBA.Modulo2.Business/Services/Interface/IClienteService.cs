using MBA.Modulo2.Data.Domain;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task UpdateAsync(Cliente cliente);
        Task AddAsync(Cliente customer);
    }
}
