using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data.Repository.Implamentation
{
    public class FavoritoRepository : Repository<Favorito>, IFavoritoRepository
    {
        private readonly AppDbContext _context;
        public FavoritoRepository(AppDbContext context) : base(context) => _context = context;

        public async Task ExcluirProdutoFavorito(Guid idCliente, Guid idProduto)
        {
            var favorito = await _context.Favoritos.FirstOrDefaultAsync(f => f.ProdutoId == idProduto && f.ClienteId == idCliente);
            _context.Remove(favorito);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirTodosProdutosFavoritoAsync(Guid idCliente)
        {
            var favoritos = await _context.Favoritos.Where(f => f.ClienteId == idCliente).ToListAsync();
            _context.RemoveRange(favoritos);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Favorito>> PegarPorIdFavoritosClienteAsync(Guid idCliente)
        {
            return await _context.Favoritos.Include(f => f.Produto).Where(f => f.ClienteId == idCliente).ToListAsync();
        }

        public async Task<Favorito> PegarPorIdProdutoFavoritoAsync(Guid idCliente, Guid idProduto)
        {
            return await _context.Favoritos.FirstOrDefaultAsync(f => f.ProdutoId == idProduto && f.ClienteId == idCliente);
        }
    }
}
