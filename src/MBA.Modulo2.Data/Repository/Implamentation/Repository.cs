using MBA.Modulo2.Data.Interface;
using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data.Repository.Implamentation;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> PegarTodosAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T> PegarPorIdAsync(Guid id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }
    public async Task<T> PegarPorAspNetUserIdAsync(Guid applicationUserID)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "ApplicationUserId") == applicationUserID);
    }

    public async Task<T> PegarPorNomeAsync(string name)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<string>(e, "Nome") == name);
    }

    public async Task AdicionaAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AlteraAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluiAsync(Guid id)
    {
        var entity = await PegarPorIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}