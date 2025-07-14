using MBA.Modulo2.Data.Domain;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IClienteService
    {
        Task AddAsync(Cliente customer);
    }
}
