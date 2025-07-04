using AutoMapper;
using MBA.Modulo2.App.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MBA.Modulo2.App.Controllers;

public class HomeController(IProdutoService productService, IMapper mapper) : Controller
{
    private readonly IProdutoService _productService = productService;
    private readonly IMapper _mapper = mapper;

     public IActionResult Index()
    {
        return View();
    }
    
    
    [Authorize]
    public async Task<IActionResult> Vitrine()
    {
        return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _productService.GetAllWithCategoryAsync()));
    }

    public IActionResult Privacy()
    {
        return View();
    }
 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
