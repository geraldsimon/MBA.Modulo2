using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data.Repository.Implamentation
{
    public class FavoritoRepository : Repository<Favorito>, IFavoritoRepository
    {
        private readonly AppDbContext _context;
        public FavoritoRepository(AppDbContext context) : base(context) => _context = context;

        public async Task DeletarProdutoFavorito(Guid idCliente, Guid idProduto)
        {
            var favorito = await _context.Favoritos.FirstOrDefaultAsync(f => f.ProdutoId == idProduto && f.ClienteId == idCliente);
            _context.Remove(favorito);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarTodosProdutosFavoritoAsync(Guid idCliente)
        {
            var favoritos = await _context.Favoritos.Where(f => f.ClienteId == idCliente).ToListAsync();
            _context.RemoveRange(favoritos);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Favorito>> GetByIdFavoritosClienteAsync(Guid idCliente)
        {
            return await _context.Favoritos.Where(f => f.ClienteId == idCliente).ToListAsync();
        }

        public async Task<Favorito> GetByIdProdutoFavoritoAsync(Guid idCliente, Guid idProduto)
        {
            return await _context.Favoritos.FirstOrDefaultAsync(f => f.ProdutoId == idProduto && f.ClienteId == idCliente);
        }
    }
}
