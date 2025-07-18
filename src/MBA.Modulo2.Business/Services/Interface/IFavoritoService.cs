using MBA.Modulo2.Data.Domain;

namespace MBA.Modulo2.Business.Services.Interface
{
    public interface IFavoritoService
    {
        Task AddAsync(Favorito favorito);

        Task<IEnumerable<Favorito>> GetAllAsync();
    }
}
