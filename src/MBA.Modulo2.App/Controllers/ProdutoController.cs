using AppSemTemplate.Extensions;
using AutoMapper;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Functions;
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
                               ICategoryService categoryService,
                               IImageService imageService,
                               IMapper mapper) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IProdutoService _productService = productService;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IImageService _imageService = imageService;
    private readonly IMapper _mapper = mapper;

    //[ClaimsAuthorize("Produtos", "VI,MVI")]
    public async Task<IActionResult> Index(string Id)
    {
        if (String.IsNullOrEmpty(Id))
        {
            Id = TempData["UserID"] as string ?? TempData["UserID"]?.ToString();

            if (String.IsNullOrEmpty(Id))
                Id = _userManager.GetUserId(User);
            else
                TempData["UserID"] = Id;
        }
        else
            TempData["UserID"] = Id;

        var products = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _productService.GetAllWithCategoryByVendedorAsync(Guid.Parse(Id)));

        foreach (var prodct in products)
        {
            prodct.Description = prodct.Description.Truncate(45);
        }

        return View(products);
    }


    public async Task<IActionResult> ToggleStatus(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = await _productService.GetByIdAsync(id);

        if (product == null) return NotFound();

        product.Active = !product.Active;
        await _productService.UpdateAsync(product);

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

        var product = _mapper.Map<ProdutoViewModel>(await _productService.GetByIdAsync(id));

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [ClaimsAuthorize("Produtos", "AD")]
    public async Task<IActionResult> Create()
    {
        var category = await _categoryService.GetAllAsync();

        if (!category.Any())
        {
            TempData["Erro"] = "Categorias must be registered in advance to associate products.\r\n Please register at least one category.";
            return RedirectToAction("Index", "Category");
        }

        ViewBag.Categorias = new SelectList(category, "Id", "Name");
        ViewData["VendedorId"] = _userManager.GetUserId(User);

        return View();
    }

    [ClaimsAuthorize("Produtos", "AD")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Stock,CategoryId,VendedorId")] ProdutoViewModel product, IFormFile ImageFile)
    {
        var _product = _mapper.Map<Produto>(product);

        if (ModelState.IsValid)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                var fileName = $"{imgPrefixo}{ImageFile.FileName}".Trim();
                await _imageService.SaveImageAsync(ImageFile, fileName);
                _product.Image = fileName;
                _product.Active = true;
            }
            product.Id = Guid.NewGuid();
            await _productService.AddAsync(_product);
            return RedirectToAction(nameof(Index));
        }
        var categories = await _categoryService.GetAllAsync();

        ViewData["CategoryId"] = new SelectList(categories, "Id", "Id", product.CategoryId);
        ViewData["Name"] = new SelectList(categories, "Name", "Name", product.Category);
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

        var product = _mapper.Map<ProdutoViewModel>(await _productService.GetByIdAsync(id));

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categorias = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
        ViewData["VendedorId"] = _userManager.GetUserId(User);
        return View(product);
    }

    [ClaimsAuthorize("Produtos", "ED")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Required] Guid id, [Bind("Id,Name,Description,Price,Stock,CategoryId,VendedorId")] ProdutoViewModel product, IFormFile ImageFile, string CurrentImage)
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
            product.Image = fileName;
        }
        else
        {
            product.Image = CurrentImage;
        }

        var _product = _mapper.Map<Produto>(product);

        if (ModelState.IsValid)
        {
            try
            {
                await _productService.UpdateAsync(_product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(product.Id))
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

        var categories = await _categoryService.GetAllAsync();
        ViewData["CategoryId"] = new SelectList(categories, "Id", "Id", product.CategoryId);
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

        var product = _mapper.Map<ProdutoViewModel>(await _productService.GetByIdAsync(id));

        if (product == null)
        {
            return NotFound();
        }

        return View("Delete", product);
    }

    [ClaimsAuthorize("Produtos", "EX")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var entity = await _productService.GetByIdAsync(id);

        if (await _productService.DeleteAsync(id))
        {
            _imageService.DeleteFile(entity.Image);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> ProductExists(Guid id)
    {
        return await _productService.GetAnyAsync(id);
    }
}