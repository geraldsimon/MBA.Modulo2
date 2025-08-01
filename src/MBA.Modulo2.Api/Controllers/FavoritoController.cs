
using AutoMapper;
using MBA.Modulo2.Api.ViewModels;
using MBA.Modulo2.Business.Functions;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Authorization;
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
            // pegar o id do cliente 
            favoritoViewModel.ClienteId = FuncoesGerais.PegarOIdDoClientePeloToken(Request.Headers.Authorization.ToString());
            if (await _favoritoService.ProdutoJaFavoritadoAsync(favoritoViewModel.ClienteId, favoritoViewModel.ProdutoId))
            {
                return NoContent();
            }
            await _favoritoService.AdicionaAsync(_mapper.Map<Favorito>(favoritoViewModel));

            return CreatedAtAction(nameof(Add), new { id = favoritoViewModel.ProdutoId }, favoritoViewModel);
        }

        [HttpDelete("{idProduto:guid}")]
        public async Task<ActionResult<FavoritoViewModel>> Delete(Guid idProduto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var ClienteId = FuncoesGerais.PegarOIdDoClientePeloToken(Request.Headers.Authorization.ToString());

            var favorito = await GetByIdAsync(ClienteId, idProduto);

            if (favorito == null)
            {
                return BadRequest();
            }                

            await _favoritoService.ExcluirProdutoFavorito(ClienteId, idProduto);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoritoDoClienteViewModel>>> GetFavoritosCliente()
        {
            var ClienteId = FuncoesGerais.PegarOIdDoClientePeloToken(Request.Headers.Authorization.ToString());

            var favoritos = await _favoritoService.PegarPorIdFavoritosClienteAsync(ClienteId);

            var favoritosViewModel = _mapper.Map<IEnumerable<FavoritoDoClienteViewModel>>(favoritos);

            return Ok(favoritosViewModel);
        }

        private async Task<FavoritoViewModel> GetByIdAsync(Guid idCliente, Guid idProduto)
        {
            return _mapper.Map<FavoritoViewModel>(await _favoritoService.PegarPorIdProdutoFavoritoAsync(idCliente, idProduto));
        }

        
    }
}
