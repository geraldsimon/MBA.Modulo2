using AppSemTemplate.Extensions;
using AutoMapper;
using MBA.Modulo2.App.Configuration;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MBA.Modulo2.App.Controllers;

[Authorize]
public class VendedorController(INotifier notifier,
                                AppState appState,
                                SignInManager<ApplicationUser> signInManager,
                                IVendedorService vendedorService,
                                IProdutoRepository produtoRepository,
                                IMapper mapper,
                               IHttpContextAccessor httpContextAccessor) : MainController(notifier, appState, signInManager, vendedorService, httpContextAccessor)
{
    private readonly IVendedorService _vendedorService = vendedorService;
    private readonly IProdutoRepository _produtoRepository = produtoRepository;
    private readonly IMapper _mapper = mapper;

    [ClaimsAuthorize("Vendedores", "VI")]
    public async Task<IActionResult> Index()
    {
        var vendedores = _mapper.Map<IEnumerable<VendedorViewModel>>(await _vendedorService.PegarTodosAsync());

        return View(vendedores);
    }

    [ClaimsAuthorize("Vendedores", "ED")]
    public async Task<IActionResult> ToggleStatus(string id)
    {
        var _vendedor = await _vendedorService.PegarPorIdComProdutosAsync(Guid.Parse(id));

        if (_vendedor == null) return NotFound();

        _vendedor.Ativo = !_vendedor.Ativo;
        await _vendedorService.AlteraAsync(_vendedor);

        foreach (var produto in _vendedor.Produtos)
        {
            produto.Ativo = _vendedor.Ativo;
            await _produtoRepository.AlteraAsync(produto);
        }

        return RedirectToAction("Index");
    }
}