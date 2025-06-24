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

public class ProdutoController(UserManager<ApplicationUser> userManager,
                               IProductService productService,
                               ICategoryService categoryService,
                               IImageService imageService,
                               IMapper mapper) : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IProductService _productService = productService;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IImageService _imageService = imageService;
    private readonly IMapper _mapper = mapper;

    public async Task<IActionResult> Index()
    {
        var user = _userManager.GetUserId(User);
       
        var products = _mapper.Map<IEnumerable<ProductViewModel>>(await _productService.GetAllWithCategoryBySellerAsync(Guid.Parse(user)));

        foreach (var prodct in products)
        {
            prodct.Description = prodct.Description.Truncate(45);
        }

        return View(products);
    }

    [Authorize]
    public async Task<IActionResult> Details([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = _mapper.Map<ProductViewModel>(await _productService.GetByIdAsync(id));

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [Authorize]
    public async Task<IActionResult> Create()
    {
        var category = await _categoryService.GetAllAsync();

        if (!category.Any())
        {
            TempData["Erro"] = "Categories must be registered in advance to associate products.\r\n Please register at least one category.";
            return RedirectToAction("Index", "Category");
        }

        ViewBag.Categories = new SelectList(category, "Id", "Name");
        ViewData["SellerId"] = _userManager.GetUserId(User);

        return View();
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Stock,CategoryId,SellerId")] ProductViewModel product, IFormFile ImageFile)
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
            }

            product.Id = Guid.NewGuid();
            await _productService.AddAsync(_product);
            return RedirectToAction(nameof(Index));
        }
        var categories = await _categoryService.GetAllAsync();

        ViewData["CategoryId"] = new SelectList(categories, "Id", "Id", product.CategoryId);
        ViewData["Name"] = new SelectList(categories, "Name", "Name", product.Category);
        ViewData["SellerId"] = _userManager.GetUserId(User);
        return View(_product);
    }

    [Authorize]
    public async Task<IActionResult> Edit([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = _mapper.Map<ProductViewModel>(await _productService.GetByIdAsync(id));

        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
        ViewData["SellerId"] = _userManager.GetUserId(User);
        return View(product);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Required] Guid id, [Bind("Id,Name,Description,Price,Stock,CategoryId,SellerId")] ProductViewModel product, IFormFile ImageFile, string CurrentImage)
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
        ViewData["SellerId"] = _userManager.GetUserId(User);
        return View(_product);
    }

    [Authorize]
    public async Task<IActionResult> Delete([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = _mapper.Map<ProductViewModel>(await _productService.GetByIdAsync(id));

        if (product == null)
        {
            return NotFound();
        }

        return View("Delete", product);
    }

    [Authorize]
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