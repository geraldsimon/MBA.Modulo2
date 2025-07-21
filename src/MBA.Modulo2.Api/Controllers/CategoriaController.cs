using AutoMapper;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Models;
using MBA.Modulo2.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MBA.Modulo2.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class CategoriaController(ICategoryService categoriaService,
                                  IMapper mapper,
                                  INotifier notifier) : MainController(notifier)
{
    private readonly ICategoryService _categoriaService = categoriaService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IEnumerable<CategoriaViewModel>> GetAll()
    {
        return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoriaViewModel>> GetById(Guid id)
    {
        var categoriaViewModel = await GetByIdAsync(id);

        if (categoriaViewModel == null)
        {
            ReportError("This Category does not exist.");
            return CustomResponse();
        }

        return categoriaViewModel;
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>> Add(CategoriaViewModel categoriaViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _categoriaService.AddAsync(_mapper.Map<Categoria>(categoriaViewModel));

        return CustomResponse(categoriaViewModel);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, CategoriaViewModel categoriaViewModel)
    {
        if (id != categoriaViewModel.Id)
        {
            ReportError("The IDs provided are not the same");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var categoriaUpdate = await _categoriaService.GetByIdWithProdutoAsync(id);

        if (categoriaUpdate == null)
        {
            ReportError("This categoria does not exist");
            return CustomResponse();
        }
        
            await _categoriaService.UpdateAsync(_mapper.Map<Categoria>(categoriaViewModel));

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<CategoriaViewModel>> DeleteAsync(Guid id)
    {
        var categoria = await GetByIdAsync(id);

        if (categoria == null)
        {
            ReportError("This categoria does not exist");
            return CustomResponse();
        }

        await _categoriaService.DeleteAsync(id);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    private async Task<CategoriaViewModel> GetByIdAsync(Guid id)
    {
        return _mapper.Map<CategoriaViewModel>(await _categoriaService.GetByIdWithProdutoAsync(id));
    }
}
