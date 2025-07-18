using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Repository.Interface;

namespace MBA.Modulo2.Data.Repository.Implamentation
{
    public class FavoritoRepository : Repository<Favorito>, IFavoritoRepository
    {
        private readonly AppDbContext _context;
        public FavoritoRepository(AppDbContext context) : base(context) => _context = context;
    }
}
