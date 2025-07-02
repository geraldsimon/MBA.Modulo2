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
public class CategoriaController(ICategoryService categoryService,
                                  IMapper mapper,
                                  INotifier notifier) : MainController(notifier)
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IEnumerable<CategoriaViewModel>> GetAll()
    {
        return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoryService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoriaViewModel>> GetById(Guid id)
    {
        var categoryViewModel = await GetByIdAsync(id);

        if (categoryViewModel == null)
        {
            ReportError("This Category does not exist.");
            return CustomResponse();
        }

        return categoryViewModel;
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>> Add(CategoriaViewModel categoryViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _categoryService.AddAsync(_mapper.Map<Categoria>(categoryViewModel));

        return CustomResponse(categoryViewModel);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, CategoriaViewModel categoryViewModel)
    {
        if (id != categoryViewModel.Id)
        {
            ReportError("The IDs provided are not the same");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var categoryUpdate = await _categoryService.GetByIdWithProductAsync(id);

        if (categoryUpdate == null)
        {
            ReportError("This category does not exist");
            return CustomResponse();
        }
        
            await _categoryService.UpdateAsync(_mapper.Map<Categoria>(categoryViewModel));

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<CategoriaViewModel>> DeleteAsync(Guid id)
    {
        var category = await GetByIdAsync(id);

        if (category == null)
        {
            ReportError("This category does not exist");
            return CustomResponse();
        }

        await _categoryService.DeleteAsync(id);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    private async Task<CategoriaViewModel> GetByIdAsync(Guid id)
    {
        return _mapper.Map<CategoriaViewModel>(await _categoryService.GetByIdWithProductAsync(id));
    }
}
