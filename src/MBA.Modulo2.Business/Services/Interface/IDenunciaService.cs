using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Domain.Enums;

namespace MBA.Modulo2.Business.Services.Interface;

public interface IDenunciaService
{
	Task<Guid> AdicionarDenunciaAsync(Denuncia denuncia);
	Task<bool> ProcessarDenunciaAsync(Guid denunciaId, StatusDenuncia novoStatus, string observacao, Guid adminId);
	Task<IEnumerable<Denuncia>> ObterDenunciasPorProdutoAsync(Guid produtoId);
	Task<IEnumerable<Denuncia>> ObterDenunciasPendentesAsync();
	Task<IEnumerable<Denuncia>> ObterDenunciasPorStatusAsync(StatusDenuncia status);
	Task<Denuncia?> ObterDenunciaPorIdAsync(Guid id);
	Task<Guid> UsuarioJaDenunciouProdutoAsync(Guid usuarioId, Guid produtoId);
}