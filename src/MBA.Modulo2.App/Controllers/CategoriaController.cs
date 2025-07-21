using AppSemTemplate.Extensions;
using AutoMapper;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.App.Controllers;

[Authorize]
public class CategoriaController(ICategoryService categoriaService, IMapper mapper) : Controller
{
    private readonly ICategoryService _categoriaService = categoriaService;
    private readonly IMapper _mapper = mapper;

    [ClaimsAuthorize("Categorias", "VI")]
    public async Task<IActionResult> Index()
    {
        return View(_mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaService.GetAllAsync()));
    }

    [Authorize]
    public async Task<IActionResult> Details([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaService.GetByIdAsync(id));

        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    [ClaimsAuthorize("Categorias", "AD")]
    public IActionResult Create()
    {
        return View();
    }

    [ClaimsAuthorize("Categorias","AD")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description")] CategoriaViewModel categoria)
    {
        if (ModelState.IsValid)
        {
            var categoriaExists = _mapper.Map<Categoria>(await _categoriaService.GetByNameAsync(categoria.Name));

            if (categoriaExists != null)
            {

                ModelState.AddModelError("Name", "There is already a categoria with this name.");
            }
            else
            {

                await _categoriaService.AddAsync(_mapper.Map<Categoria>(categoria));
                return RedirectToAction(nameof(Index));
            }
        }
        return View(categoria);
    }

    [ClaimsAuthorize("Categorias", "ED")]
    public async Task<IActionResult> Edit([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaService.GetByIdAsync(id));
        if (categoria == null)
        {
            return NotFound();
        }

        var _categoria = new CategoriaViewModel
        {
            Name = categoria.Name,
            Description = categoria.Description
        };

        return View(_categoria);
    }

    [ClaimsAuthorize("Categorias", "ED")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description")] CategoriaViewModel categoria)
    {
        if (id != categoria.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _categoriaService.UpdateAsync(_mapper.Map<Categoria>(categoria));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(categoria.Id))
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
        return View(categoria);
    }

    [ClaimsAuthorize("Categorias", "EX")]
    public async Task<IActionResult> Delete([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaService.GetByIdWithProdutoAsync(id));

        if (categoria == null)
        {
            return NotFound();
        }

        if (categoria.Produtos.Any())
        {
            TempData["Erro"] = $"The {categoria.Name} categoria cannot be deleted as it is already associated with products.";
            return RedirectToAction("Index");
        }

        return View(categoria);
    }


    [ClaimsAuthorize("Categorias", "EX")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = await _categoriaService.GetByIdAsync(id);
        if (categoria != null)
        {
            await _categoriaService.DeleteAsync(id);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> CategoryExists(Guid id)
    {
        return await _categoriaService.GetByIdAsync(id) != null;
    }
}
