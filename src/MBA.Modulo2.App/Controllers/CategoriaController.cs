using AppSemTemplate.Extensions;
using AutoMapper;
using MBA.Modulo2.App.Configuration;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MBA.Modulo2.App.Controllers;

[Authorize]
public class CategoriaController(INotifier notifier,
                                 AppState appState,
                                 UserManager<ApplicationUser> userManager,
                                 IVendedorService vendedorService, 
                                 ICategoriaService categoriaService, 
                                 IMapper mapper) : MainController(notifier, appState, userManager, vendedorService)
{
    private readonly ICategoriaService _categoriaService = categoriaService;
    private readonly IMapper _mapper = mapper;

    [ClaimsAuthorize("Categorias", "VI")]
    public async Task<IActionResult> Index()
    {
        return View(_mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaService.PegarTodosAsync()));
    }

    [Authorize]
    public async Task<IActionResult> Details([Required] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaService.PegarPorIdAsync(id));

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
    public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] CategoriaViewModel categoria)
    {
        if (ModelState.IsValid)
        {
            var categoriaExists = _mapper.Map<Categoria>(await _categoriaService.PegarPorNomeAsync(categoria.Nome));

            if (categoriaExists != null)
            {

                ModelState.AddModelError("Nome", "Já existe uma categoria com este nome.");
            }
            else
            {

                await _categoriaService.AdicionaAsync(_mapper.Map<Categoria>(categoria));
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

        var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaService.PegarPorIdAsync(id));
        if (categoria == null)
        {
            return NotFound();
        }

        var _categoria = new CategoriaViewModel
        {
            Nome = categoria.Nome,
            Descricao = categoria.Descricao
        };

        return View(_categoria);
    }

    [ClaimsAuthorize("Categorias", "ED")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Descricao")] CategoriaViewModel categoria)
    {
        if (id != categoria.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _categoriaService.AlteraAsync(_mapper.Map<Categoria>(categoria));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoriaExiste(categoria.Id))
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

        var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaService.PegarPorIdComProdutoAsync(id));

        if (categoria == null)
        {
            return NotFound();
        }

        if (categoria.Produtos.Any())
        {
            TempData["Erro"] = $"A categoria {categoria.Nome} não pode ser excluída, pois já está associada a produtos.";
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

        var categoria = await _categoriaService.PegarPorIdAsync(id);
        if (categoria != null)
        {
            await _categoriaService.ExcluiAsync(id);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> CategoriaExiste(Guid id)
    {
        return await _categoriaService.PegarPorIdAsync(id) != null;
    }
}
