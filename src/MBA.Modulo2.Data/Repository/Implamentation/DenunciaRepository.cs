using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Domain.Enums;
using MBA.Modulo2.Data.Repository.Interface;

using Microsoft.EntityFrameworkCore;

namespace MBA.Modulo2.Data.Repository.Implamentation;

public class DenunciaRepository : Repository<Denuncia>, IDenunciaRepository
{
	private readonly AppDbContext _context;

	public DenunciaRepository(AppDbContext context) : base(context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Denuncia>> ObterDenunciasPorStatusAsync(StatusDenuncia status)
	{
		return await _context.Denuncias
			.AsNoTracking()
			.Include(d => d.Produto)
			.Include(d => d.Denunciante)
			.Include(d => d.AdminResponsavel)
			.Where(d => d.Status == status)
			.OrderByDescending(d => d.DataCriacao)
			.ToListAsync();
	}

	public async Task<IEnumerable<Denuncia>> ObterDenunciasPorProdutoAsync(Guid produtoId)
	{
		return await _context.Denuncias
			.AsNoTracking()
			.Include(d => d.Denunciante)
			.Include(d => d.AdminResponsavel)
			.Where(d => d.ProdutoId == produtoId)
			.OrderByDescending(d => d.DataCriacao)
			.ToListAsync();
	}

	public async Task<Guid> UsuarioJaDenunciouProdutoAsync(Guid usuarioId, Guid produtoId)
	{
		var denuncia = await _context.Denuncias
			.AsNoTracking()
			.FirstOrDefaultAsync(d => d.DenuncianteId == usuarioId && d.ProdutoId == produtoId);

		return denuncia?.Id ?? Guid.Empty;
	}

	public async Task<IEnumerable<Denuncia>> ObterDenunciasPendentesAsync()
	{
		return await ObterDenunciasPorStatusAsync(StatusDenuncia.Pendente);
	}
}