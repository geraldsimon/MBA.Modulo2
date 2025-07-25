using AppSemTemplate.Extensions;
using AutoMapper;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Functions;
using MBA.Modulo2.Business.Services.Implamentation;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.App.Controllers;

[Authorize]
public class ProdutoController(UserManager<ApplicationUser> userManager,
                               IProdutoService productService,
                               ICategoriaService categoriaService,
                               IImageService imageService,
                               IMapper mapper) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IProdutoService _productService = productService;
    private readonly ICategoriaService _categoriaService = categoriaService;
    private readonly IImageService _imageService = imageService;
    private readonly IMapper _mapper = mapper;

    //[ClaimsAuthorize("Produtos", "VI,MVI")]
    public async Task<IActionResult> Index(string Id)
    {
        if (String.IsNullOrEmpty(Id))
        {
            Id = TempData["_UserID"] as string ?? TempData["_UserID"]?.ToString().ToUpper();

            if (String.IsNullOrEmpty(Id))
                Id = Guid.Parse(_userManager.GetUserId(User)).ToString().ToUpper();
            else
                TempData["_UserID"] = Id;
        }
        else
            TempData["_UserID"] = Id;

        var products = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _productService.PegaTodosComCategoriasPorVendedorAsync(Guid.Parse(Id)));

        foreach (var prodct in products)
        {
            prodct.Descricao = prodct.Descricao.Truncate(45);
        }

        return View(products);
    }


    public async Task<IActionResult> ToggleStatus(Guid? id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = await _productService.PegaPorIdAsync(id);

        if (product == null) return NotFound();

        product.Ativo = !product.Ativo;
        await _productService.AlteraAsync(product);

        return RedirectToAction("Index"); // ou o nome da sua lista
    }

    //[ClaimsAuthorize("Produtos", "VI")]
    [Authorize]
    public async Task<IActionResult> Details([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = _mapper.Map<ProdutoViewModel>(await _productService.PegaPorIdAsync(id));

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [ClaimsAuthorize("Produtos", "AD")]
    public async Task<IActionResult> Create()
    {
        var categoria = await _categoriaService.PegarTodosAsync();

        if (!categoria.Any())
        {
            TempData["Erro"] = "As categorias devem ser registradas com antecedência para associar produtos.\r\nRegistre pelo menos uma categoria.";
            return RedirectToAction("Index", "Categoria");
        }

        ViewBag.Categorias = new SelectList(categoria, "Id", "Nome");
        ViewData["VendedorId"] = _userManager.GetUserId(User);

        return View();
    }

    [ClaimsAuthorize("Produtos", "AD")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Preco,Estoque,CategoriaId,VendedorId")] ProdutoViewModel product, IFormFile ImageFile)
    {
        var _product = _mapper.Map<Produto>(product);

        if (ModelState.IsValid)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                var fileName = $"{imgPrefixo}{ImageFile.FileName}".Trim();
                await _imageService.SaveImageAsync(ImageFile, fileName);
                _product.Imagem = fileName;
                _product.VendedorId = Guid.Parse(_userManager.GetUserId(User));
            }
            product.Id = Guid.NewGuid();
            await _productService.AdicionaAsync(_product);
            return RedirectToAction(nameof(Index));
        }
        var categories = await _categoriaService.PegarTodosAsync();

        ViewData["CategoryId"] = new SelectList(categories, "Id", "Id", product.CategoriaId);
        ViewData["Nome"] = new SelectList(categories, "Nome", "Nome", product.Categoria);
        ViewData["VendedorId"] = _userManager.GetUserId(User);
        return View(_product);
    }

    [ClaimsAuthorize("Produtos", "ED")]
    public async Task<IActionResult> Edit([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = _mapper.Map<ProdutoViewModel>(await _productService.PegaPorIdAsync(id));

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categorias = new SelectList(await _categoriaService.PegarTodosAsync(), "Id", "Nome");
        ViewData["VendedorId"] = _userManager.GetUserId(User);
        return View(product);
    }

    [ClaimsAuthorize("Produtos", "ED")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Required] Guid id, [Bind("Id,Nome,Descricao,Preco,Estoque,CategoriaId,VendedorId")] ProdutoViewModel product, IFormFile ImageFile, string CurrentImage)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (ImageFile != null && ImageFile.Length > 0)
        {
            var imgPrefixo = Guid.NewGuid() + "_";
            var fileName = $"{imgPrefixo}{ImageFile.FileName}".Trim();
            await _imageService.SaveImageAsync(ImageFile, fileName);
            product.Imagem = fileName;
        }
        else
        {
            product.Imagem = CurrentImage;
        }

        var _product = _mapper.Map<Produto>(product);

        if (ModelState.IsValid)
        {
            try
            {
                await _productService.AlteraAsync(_product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProdutoExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        var categories = await _categoriaService.PegarTodosAsync();
        ViewData["CategoryId"] = new SelectList(categories, "Id", "Id", product.CategoriaId);
        ViewData["VendedorId"] = _userManager.GetUserId(User);
        return View(_product);
    }

    [ClaimsAuthorize("Produtos", "EX")]
    public async Task<IActionResult> Delete([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = _mapper.Map<ProdutoViewModel>(await _productService.PegaPorIdAsync(id));

        if (product == null)
        {
            return NotFound();
        }

        return View("Delete", product);
    }

    [ClaimsAuthorize("Produtos", "EX")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid? id)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var entity = await _productService.PegaPorIdAsync(id);

        if (await _productService.ExcluiAsync((Guid)id))
        {
            _imageService.DeleteFile(entity.Imagem);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> ProdutoExists(Guid id)
    {
        return await _productService.PegaPorIdAsync(id);
    }

    public async Task<IActionResult> ToggleStatusProduto(string id)
    {

        Guid? guid = Guid.Parse(id);
        var _produto = await _productService.PegaPorIdAsync(guid);

        if (_produto == null) return NotFound();

        _produto.Ativo = !_produto.Ativo;
        await _productService.AlteraAsync(_produto);

        return RedirectToAction("Index");
    }
}