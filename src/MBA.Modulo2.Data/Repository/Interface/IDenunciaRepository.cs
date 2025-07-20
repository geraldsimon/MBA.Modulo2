using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Domain.Enums;
using MBA.Modulo2.Data.Interface;

namespace MBA.Modulo2.Data.Repository.Interface;

public interface IDenunciaRepository : IRepository<Denuncia>
{
	Task<IEnumerable<Denuncia>> ObterDenunciasPorStatusAsync(StatusDenuncia status);
	Task<IEnumerable<Denuncia>> ObterDenunciasPorProdutoAsync(Guid produtoId);
	Task<Guid> UsuarioJaDenunciouProdutoAsync(Guid usuarioId, Guid produtoId);
	Task<IEnumerable<Denuncia>> ObterDenunciasPendentesAsync();
}