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
public class CategoryController(ICategoryService categoryService,
                                  IMapper mapper,
                                  INotifier notifier) : MainController(notifier)
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IEnumerable<CategoryViewModel>> GetAll()
    {
        return _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryViewModel>> GetById(Guid id)
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
    public async Task<ActionResult<CategoryViewModel>> Add(CategoryViewModel categoryViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _categoryService.AddAsync(_mapper.Map<Category>(categoryViewModel));

        return CustomResponse(categoryViewModel);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, CategoryViewModel categoryViewModel)
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
        
            await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryViewModel));

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<CategoryViewModel>> DeleteAsync(Guid id)
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

    private async Task<CategoryViewModel> GetByIdAsync(Guid id)
    {
        return _mapper.Map<CategoryViewModel>(await _categoryService.GetByIdWithProductAsync(id));
    }
}
