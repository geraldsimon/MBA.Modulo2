using AutoMapper;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.App.Controllers;

public class CategoriaController(ICategoryService categoryService, IMapper mapper) : Controller
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IMapper _mapper = mapper;

    [Authorize]
    public async Task<IActionResult> Index()
    {
        return View(_mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoryService.GetAllAsync()));
    }

    [Authorize]
    public async Task<IActionResult> Details([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = _mapper.Map<CategoriaViewModel>(await _categoryService.GetByIdAsync(id));

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description")] CategoriaViewModel category)
    {
        if (ModelState.IsValid)
        {
            var categoryExists = _mapper.Map<Categoria>(await _categoryService.GetByNameAsync(category.Name));

            if (categoryExists != null)
            {

                ModelState.AddModelError("Name", "There is already a category with this name.");
            }
            else
            {

                await _categoryService.AddAsync(_mapper.Map<Categoria>(category));
                return RedirectToAction(nameof(Index));
            }
        }
        return View(category);
    }

    [Authorize]
    public async Task<IActionResult> Edit([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = _mapper.Map<CategoriaViewModel>(await _categoryService.GetByIdAsync(id));
        if (category == null)
        {
            return NotFound();
        }

        var _category = new CategoriaViewModel
        {
            Name = category.Name,
            Description = category.Description
        };

        return View(_category);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description")] CategoriaViewModel category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _categoryService.UpdateAsync(_mapper.Map<Categoria>(category));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(category.Id))
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
        return View(category);
    }

    [Authorize]
    public async Task<IActionResult> Delete([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = _mapper.Map<CategoriaViewModel>(await _categoryService.GetByIdWithProductAsync(id));

        if (category == null)
        {
            return NotFound();
        }

        if (category.Products.Any())
        {
            TempData["Erro"] = $"The {category.Name} category cannot be deleted as it is already associated with products.";
            return RedirectToAction("Index");
        }

        return View(category);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = await _categoryService.GetByIdAsync(id);
        if (category != null)
        {
            await _categoryService.DeleteAsync(id);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> CategoryExists(Guid id)
    {
        return await _categoryService.GetByIdAsync(id) != null;
    }
}
