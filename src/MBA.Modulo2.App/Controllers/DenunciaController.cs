using AppSemTemplate.Extensions;

using AutoMapper;

using MBA.Modulo2.App.Extensions;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Domain.Enums;
using MBA.Modulo2.Data.Domain.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MBA.Modulo2.App.Controllers;

[Authorize]
public class DenunciaController : Controller
{
	private readonly IDenunciaService _denunciaService;
	private readonly IProdutoService _produtoService;
	private readonly INotifier _notifier;
	private readonly IMapper _mapper;

	public DenunciaController(IDenunciaService denunciaService,
		IProdutoService produtoService,
		INotifier notifier,
		IMapper mapper)
	{
		_denunciaService = denunciaService;
		_produtoService = produtoService;
		_notifier = notifier;
		_mapper = mapper;
	}

	[ClaimsAuthorize("Admin", "Admin")]
	public async Task<IActionResult> Index()
	{
		var denuncias = await _denunciaService.ObterDenunciasPendentesAsync();
		var denunciasViewModel = _mapper.Map<IEnumerable<DenunciaViewModel>>(denuncias);
		return View(denunciasViewModel);
	}

	[ClaimsAuthorize("Admin", "Admin")]
	public async Task<IActionResult> Details(Guid id)
	{
		var denuncia = await _denunciaService.ObterDenunciaPorIdAsync(id);
		if (denuncia == null) return NotFound();

		var denunciaViewModel = _mapper.Map<DenunciaViewModel>(denuncia);
		return View(denunciaViewModel);
	}

	public async Task<IActionResult> Create(Guid produtoId)
	{
		var produto = await _produtoService.GetByIdAsync(produtoId);
		if (produto == null) return NotFound();

		var usuarioId = Guid.Parse(User.GetUserId());
		var denunciaId = await _denunciaService.UsuarioJaDenunciouProdutoAsync(usuarioId, produtoId);

		if (denunciaId != Guid.Empty)
		{
			TempData["Erro"] = "Você já denunciou este produto anteriormente.";
			return RedirectToAction("Details", "Produto", new { id = produtoId });
		}

		var viewModel = new CriarDenunciaViewModel
		{
			ProdutoId = produtoId,
			ProdutoNome = produto.Name
		};

		ViewBag.Tipos = Enum.GetValues<TipoDenuncia>()
			.Select(td
				=> new SelectListItem
				{
					Value = ((short)td).ToString(),
					Text = td.ToDescription()
				});

		return View(viewModel);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CriarDenunciaViewModel viewModel)
	{
		if (!ModelState.IsValid)
		{
			var produto = await _produtoService.GetByIdAsync(viewModel.ProdutoId);
			viewModel.ProdutoNome = produto?.Name;
			return View(viewModel);
		}

		var denuncia = _mapper.Map<Denuncia>(viewModel);
		denuncia.DenuncianteId = Guid.Parse(User.GetUserId());

		var denunciaId = await _denunciaService.AdicionarDenunciaAsync(denuncia);

		if (denunciaId == Guid.Empty)
		{
			foreach (var notificacao in _notifier.GetNotifications())
				ModelState.AddModelError(string.Empty, notificacao.Message);

			var produto = await _produtoService.GetByIdAsync(viewModel.ProdutoId);
			viewModel.ProdutoNome = produto?.Name;
			return View(viewModel);
		}

		TempData["Sucesso"] = "Denúncia enviada com sucesso! Será analisada pelos administradores.";
		return RedirectToAction("Details", "Produto", new { id = viewModel.ProdutoId });
	}

	[ClaimsAuthorize("Admin", "Admin")]
	public async Task<IActionResult> Processar(Guid id)
	{
		var denuncia = await _denunciaService.ObterDenunciaPorIdAsync(id);
		if (denuncia == null) return NotFound();

		var viewModel = new ProcessarDenunciaViewModel
		{
			Id = id,
			Denuncia = _mapper.Map<DenunciaViewModel>(denuncia)
		};

		return View(viewModel);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[ClaimsAuthorize("Admin", "Admin")]
	public async Task<IActionResult> Processar(ProcessarDenunciaViewModel viewModel)
	{
		if (!ModelState.IsValid)
		{
			var denuncia = await _denunciaService.ObterDenunciaPorIdAsync(viewModel.Id);
			viewModel.Denuncia = _mapper.Map<DenunciaViewModel>(denuncia);
			return View(viewModel);
		}

		var adminId = Guid.Parse(User.GetUserId());
		var resultado = await _denunciaService.ProcessarDenunciaAsync(
			viewModel.Id,
			viewModel.NovoStatus,
			viewModel.ObservacaoAdmin ?? string.Empty,
			adminId);

		if (!resultado)
		{
			foreach (var notificacao in _notifier.GetNotifications())
				ModelState.AddModelError(string.Empty, notificacao.Message);

			var denuncia = await _denunciaService.ObterDenunciaPorIdAsync(viewModel.Id);
			viewModel.Denuncia = _mapper.Map<DenunciaViewModel>(denuncia);
			return View(viewModel);
		}

		TempData["Sucesso"] = "Denúncia processada com sucesso!";
		return RedirectToAction(nameof(Index));
	}

	[ClaimsAuthorize("Admin", "Admin")]
	public async Task<IActionResult> Historico()
	{
		var todasDenuncias = new List<Denuncia>();

		var aprovadas = await _denunciaService.ObterDenunciasPorStatusAsync(StatusDenuncia.Aprovada);
		var rejeitadas = await _denunciaService.ObterDenunciasPorStatusAsync(StatusDenuncia.Rejeitada);

		todasDenuncias.AddRange(aprovadas);
		todasDenuncias.AddRange(rejeitadas);

		var denunciasViewModel = _mapper.Map<IEnumerable<DenunciaViewModel>>(
			todasDenuncias.OrderByDescending(d => d.DataResolucao));

		return View(denunciasViewModel);
	}
}