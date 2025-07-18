using AutoMapper;
using MBA.Modulo2.Api.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace MBA.Modulo2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritoController(IFavoritoService favoritoService, IMapper mapper, INotifier notifier) : MainController(notifier)
    {
        private readonly IFavoritoService _favoritoService = favoritoService;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult<FavoritoViewModel>> Add(FavoritoViewModel favoritoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _favoritoService.AddAsync(_mapper.Map<Favorito>(favoritoViewModel));

            return CustomResponse(favoritoViewModel);
        }
    }
}
