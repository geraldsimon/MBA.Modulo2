using MBA.Modulo2.Business.Notifications;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Domain.Enums;
using MBA.Modulo2.Data.Interface;
using MBA.Modulo2.Data.Repository.Interface;

namespace MBA.Modulo2.Business.Services.Implementacao;

public class DenunciaService : IDenunciaService
{
	private readonly IDenunciaRepository _denunciaRepository;
	private readonly IProdutoRepository _produtoRepository;
	private readonly INotifier _notifier;

	public DenunciaService(
		IDenunciaRepository denunciaRepository,
		IProdutoRepository produtoRepository,
		INotifier notifier)
	{
		_denunciaRepository = denunciaRepository;
		_produtoRepository = produtoRepository;
		_notifier = notifier;
	}

	public async Task<Guid> AdicionarDenunciaAsync(Denuncia denuncia)
	{
		var denunciaId =
			await _denunciaRepository.UsuarioJaDenunciouProdutoAsync(denuncia.DenuncianteId, denuncia.ProdutoId);
		if (denunciaId != Guid.Empty)
		{
			_notifier.Handle(new Notificacao("Você já denunciou este produto anteriormente."));
			return denunciaId;
		}

		var produto = await _produtoRepository.PegaPorIdAsync(denuncia.ProdutoId);
		if (produto == null)
		{
			_notifier.Handle(new Notificacao("Produto não encontrado."));
			return Guid.Empty;
		}

		await _denunciaRepository.AdicionaAsync(denuncia);
		return denuncia.Id;
	}

	public async Task<bool> ProcessarDenunciaAsync(
		Guid denunciaId,
		StatusDenuncia novoStatus,
		string observacao,
		Guid adminId)
	{
		var denuncia = await _denunciaRepository.PegarPorIdAsync(denunciaId);
		if (denuncia == null)
		{
			_notifier.Handle(new Notificacao("Denúncia não encontrada."));
			return false;
		}

		if (denuncia.Status is not StatusDenuncia.Pendente and not StatusDenuncia.EmAnalise)
		{
			_notifier.Handle(new Notificacao("Esta denúncia já foi processada."));
			return false;
		}

		denuncia.Status = novoStatus;
		denuncia.ObservacaoAdmin = observacao;
		denuncia.AdminResponsavelId = adminId;
		denuncia.DataResolucao = DateTime.UtcNow;

		if (novoStatus == StatusDenuncia.Aprovada)
		{
			var produto = await _produtoRepository.PegaPorIdAsync(denuncia?.ProdutoId);
			if (produto != null)
			{
				produto.Ativo = false;
				await _produtoRepository.AlteraAsync(produto);
			}
		}

		await _denunciaRepository.AlteraAsync(denuncia);
		return true;
	}

	public async Task<IEnumerable<Denuncia>> ObterDenunciasPorProdutoAsync(Guid produtoId)
	{
		return await _denunciaRepository.ObterDenunciasPorProdutoAsync(produtoId);
	}

	public async Task<IEnumerable<Denuncia>> ObterDenunciasPendentesAsync()
	{
		return await _denunciaRepository.ObterDenunciasPendentesAsync();
	}

	public async Task<IEnumerable<Denuncia>> ObterDenunciasPorStatusAsync(StatusDenuncia status)
	{
		return await _denunciaRepository.ObterDenunciasPorStatusAsync(status);
	}

	public async Task<Denuncia?> ObterDenunciaPorIdAsync(Guid id)
	{
		return await _denunciaRepository.PegarPorIdAsync(id);
	}

	public async Task<Guid> UsuarioJaDenunciouProdutoAsync(Guid usuarioId, Guid produtoId)
	{
		return await _denunciaRepository.UsuarioJaDenunciouProdutoAsync(usuarioId, produtoId);
	}
}