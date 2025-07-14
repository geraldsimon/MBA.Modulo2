using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Repository.Interface;

namespace MBA.Modulo2.Business.Services.Implementacao
{
    public class ClienteService(IClienteRepository customerRepository) : IClienteService
    {
        private readonly IClienteRepository _customerRepository = customerRepository;
        public async Task AddAsync(Cliente customer)
        {
            await _customerRepository.AddAsync(customer);
        }
    }
}
