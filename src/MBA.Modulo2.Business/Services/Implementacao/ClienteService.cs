using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Repository.Interface;

namespace MBA.Modulo2.Business.Services.Implementacao
{
    public class ClienteService(IClienteRepository customerRepository) : IClienteService
    {
        private readonly IClienteRepository _clienteRepository = customerRepository;

        public async Task<IEnumerable<Cliente>> PegarTodosAsync()
        {
            return await _clienteRepository.PegarTodosAsync();
        }

        public async Task<Cliente> PegarPorIdAsync(Guid id)
        {
            return await _clienteRepository.PegarPorIdAsync(id);
        }

        public async Task<bool> ExcluiAsync(Guid id)
        {
            await _clienteRepository.ExcluiAsync(id);
            return true;
        }

        public async Task AlteraAsync(Cliente cliente)
        {
            await _clienteRepository.AlteraAsync(cliente);
        }

        public async Task AdicionaAsync(Cliente customer)
        {
            await _clienteRepository.AdicionaAsync(customer);
        }
    }
}
