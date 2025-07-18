using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Repository.Interface;

namespace MBA.Modulo2.Business.Services.Implementacao
{
    public class ClienteService(IClienteRepository customerRepository) : IClienteService
    {
        private readonly IClienteRepository _clienteRepository = customerRepository;

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task<Cliente> GetByIdAsync(Guid id)
        {
            return await _clienteRepository.GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _clienteRepository.DeleteAsync(id);
            return true;
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task AddAsync(Cliente customer)
        {
            await _clienteRepository.AddAsync(customer);
        }
    }
}
