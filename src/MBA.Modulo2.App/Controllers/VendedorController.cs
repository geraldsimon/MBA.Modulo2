using AppSemTemplate.Extensions;
using AutoMapper;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBA.Modulo2.App.Controllers;

[Authorize]
public class VendedorController(IVendedorService vendedorService,
                                IProdutoRepository produtoRepository,
                               IMapper mapper) : Controller
{
    private readonly IVendedorService _vendedorService = vendedorService;
    private readonly IProdutoRepository _produtoRepository = produtoRepository;
    private readonly IMapper _mapper = mapper;

    [ClaimsAuthorize("Vendedores", "VI")]
    public async Task<IActionResult> Index()
    {
        var vendedores = _mapper.Map<IEnumerable<VendedorViewModel>>(await _vendedorService.GetAllAsync());

        return View(vendedores);
    }

    [ClaimsAuthorize("Vendedores", "ED")]
    public async Task<IActionResult> ToggleStatus(string id)
    {
        var _vendedor = await _vendedorService.GetByByIdWithProductAsync(Guid.Parse(id));

        if (_vendedor == null) return NotFound();

        _vendedor.Active = !_vendedor.Active;
        await _vendedorService.UpdateAsync(_vendedor);

        foreach (var produto in _vendedor.Products)
        {
            produto.Active = _vendedor.Active;
            await _produtoRepository.UpdateAsync(produto);
        }

        return RedirectToAction("Index"); 
    }
}