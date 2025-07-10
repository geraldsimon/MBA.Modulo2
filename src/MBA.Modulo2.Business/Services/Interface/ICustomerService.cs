using MBA.Modulo2.Data.Domain;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface ICustomerService
    {
        Task AddAsync(Cliente customer);
    }
}
