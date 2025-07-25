
using AutoMapper;
using MBA.Modulo2.Api.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

            await _favoritoService.AdicionaAsync(_mapper.Map<Favorito>(favoritoViewModel));

            return CustomResponse(favoritoViewModel);
        }

        [HttpDelete("{idCliente:guid}/{idProduto:guid}")]
        public async Task<ActionResult<FavoritoViewModel>> Delete(Guid idCliente, Guid idProduto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var favorito = await GetByIdAsync(idCliente, idProduto);

            if (favorito == null)
            {
                ReportError("Produto não encontrado");
                return CustomResponse();
            }
                

            await _favoritoService.ExcluirProdutoFavorito(idCliente, idProduto);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{idCliente:guid}")]
        public async Task<ActionResult<FavoritoViewModel>> Delete(Guid idCliente)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var favoritos = await _favoritoService.PegarPorIdFavoritosClienteAsync(idCliente);

            if (favoritos == null || favoritos.Count() <= 0)
            {
                ReportError("Nenhum produto encontrado");
                return CustomResponse();
            }

            await _favoritoService.ExcluirTodosProdutosFavoritoAsync(idCliente);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpGet("{idCliente:guid}")]
        public async Task<IEnumerable<FavoritoViewModel>> GetFavoritosCliente(Guid idCliente)
        {
            return _mapper.Map<IEnumerable<FavoritoViewModel>>(await _favoritoService.PegarPorIdFavoritosClienteAsync(idCliente));
        }

        private async Task<FavoritoViewModel> GetByIdAsync(Guid idCliente, Guid idProduto)
        {
            return _mapper.Map<FavoritoViewModel>(await _favoritoService.PegarPorIdProdutoFavoritoAsync(idCliente, idProduto));
        }

        
    }
}
