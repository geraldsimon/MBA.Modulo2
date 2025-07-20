using MBA.Modulo2.Api.Extensions;
using MBA.Modulo2.Api.ViewModels.Denuncia;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain.Enums;
using MBA.Modulo2.Data.Domain.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBA.Modulo2.Api.Controllers;

[Authorize]
[Route("api/denuncias")]
public class DenunciaController : MainController
{
	private readonly IDenunciaService _denunciaService;
	private readonly IUser _user;

	public DenunciaController(INotifier notifier,
		IDenunciaService denunciaService,
		IUser user) : base(notifier)
	{
		_denunciaService = denunciaService;
		_user = user;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<DenunciaViewModel>>> ObterTodasAsync()
	{
		var denuncias = await _denunciaService.ObterDenunciasPendentesAsync();
		return CustomResponse(denuncias.Select(d => (DenunciaViewModel)d));
	}

	[HttpGet("{productId}/produto")]
	public async Task<ActionResult<IEnumerable<DenunciaViewModel>>> ObterDenunciasPorProdutoAsync(Guid productId)
	{
		var denuncias = await _denunciaService.ObterDenunciasPorProdutoAsync(productId);
		return CustomResponse(denuncias.Select(d => (DenunciaViewModel)d));
	}

	[HttpGet("tipo-denuncia")]
	[AllowAnonymous]
	public Task<ActionResult<IEnumerable<TipoDenunciaViewModel>>> ObterTipoDenunciaAsync()
	{
		var tipos = Enum.GetValues<TipoDenuncia>()
			.Select(td
				=> new TipoDenunciaViewModel(
					(short)td,
					td.ToDescription()));

		return Task.FromResult<ActionResult<IEnumerable<TipoDenunciaViewModel>>>(CustomResponse(tipos));
	}

	[HttpGet("status-denuncia")]
	[AllowAnonymous]
	public Task<ActionResult<IEnumerable<StatusDenunciaViewModel>>> ObterStatusDenunciaAsync()
	{
		var tipos = Enum.GetValues<StatusDenuncia>()
			.Select(td
				=> new StatusDenunciaViewModel(
					(short)td,
					td.ToDescription()));

		return Task.FromResult<ActionResult<IEnumerable<StatusDenunciaViewModel>>>(CustomResponse(tipos));
	}

	[HttpGet("pendentes")]
	public async Task<ActionResult<IEnumerable<DenunciaViewModel>>> ObterPendentesAsync()
	{
		var denuncias = await _denunciaService.ObterDenunciasPendentesAsync();
		return CustomResponse(denuncias.Select(d => (DenunciaViewModel)d));
	}

	[HttpGet("status/{status}")]
	public async Task<ActionResult<IEnumerable<DenunciaViewModel>>> ObterPorStatusAsync(StatusDenuncia status)
	{
		var denuncias = await _denunciaService.ObterDenunciasPorStatusAsync(status);
		return CustomResponse(denuncias.Select(d => (DenunciaViewModel)d));
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<DenunciaViewModel>> ObterPorIdAsync(Guid id)
	{
		var denuncia = await _denunciaService.ObterDenunciaPorIdAsync(id);
		if (denuncia == null) return NotFound();

		return CustomResponse((DenunciaViewModel)denuncia);
	}

	[HttpPost]
	public async Task<ActionResult<DenunciaViewModel>> CriarAsync(CriarDenunciaViewModel viewModel)
	{
		if (!ModelState.IsValid) return CustomResponse(ModelState);

		var resultado = await _denunciaService.AdicionarDenunciaAsync(viewModel.SetDenuncianteId(_user.GetUserId()));
		if (resultado != Guid.Empty) return CustomResponse();

		return CustomResponse((DenunciaViewModel)viewModel.SetId(resultado));
	}

	[HttpPut("{id:guid}/processar")]
	[CustomAuthorize("Admin", "Admin")]
	public async Task<ActionResult> ProcessarDenunciaAsync(Guid id, ProcessarDenunciaViewModel processarViewModel)
	{
		if (!ModelState.IsValid) return CustomResponse(ModelState);

		var resultado = await _denunciaService.ProcessarDenunciaAsync(
			id,
			processarViewModel.NovoStatus,
			processarViewModel.ObservacaoAdmin ?? string.Empty,
			_user.GetUserId());

		return CustomResponse(resultado);
	}

	[HttpGet("produto/{produtoId:guid}/ja-denunciou")]
	public async Task<IActionResult> UsuarioJaDenunciou(Guid produtoId)
	{
		var denunciaId = await _denunciaService.UsuarioJaDenunciouProdutoAsync(_user.GetUserId(), produtoId);

		return CustomResponse(new
		{
			denunciado = denunciaId != Guid.Empty,
			denunciaId = denunciaId != Guid.Empty ? denunciaId : (Guid?)null
		});
	}
}