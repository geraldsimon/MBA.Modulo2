﻿using AutoMapper;
using MBA.Modulo2.Api.ViewModels;
using MBA.Modulo2.Business.Functions;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Models;
using MBA.Modulo2.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MBA.Modulo2.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ProdutoController(IProdutoService productService,
                                IImageService imageService,
                                IMapper mapper,
                                INotifier notifier) : MainController(notifier)
{
    private readonly IProdutoService _productService = productService;
    private readonly IImageService _imageService = imageService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IEnumerable<ProdutoLoggedOutViewModel>> GetAll()
    {
        var productList = _mapper.Map<IEnumerable<ProdutoLoggedOutViewModel>>(await _productService.PegarTodosComCategoriaAsync());

        foreach (var product in productList)
        {
            product.ImagemUpload = _imageService.ConvertImageToBase64(product.Imagem);
        }

        return productList;
    }

    [HttpGet("{id:guid}/categoria")]
    [AllowAnonymous]
    public async Task<IEnumerable<ProdutoLoggedOutViewModel>> PegarTodosPorCategoria(Guid id)
    {
        var produtosPorCategoriaList = _mapper.Map<IEnumerable<ProdutoLoggedOutViewModel>>(await _productService.PegarTodosPorCatgoria(id));

        foreach (var product in produtosPorCategoriaList)
        {
            product.ImagemUpload = _imageService.ConvertImageToBase64(product.Imagem);
        }

        return produtosPorCategoriaList;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProdutoViewModel>> GetByID(Guid id)
    {
        var vendedorId = FuncoesGerais.PegarOIdDoUsuarioPeloToken(Request.Headers.Authorization.ToString());

        var product = await GetProdutoByID(id, vendedorId);

        if (product == null)
        {
            ReportError("Only the user who created it can delete the record.");
            return CustomResponse();
        }

        product.ImagemUpload = _imageService.ConvertImageToBase64(product.Imagem);

        return product;
    }

    [HttpGet("{id:guid}/detalhes")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByID2(Guid id)
    {
        var product = await DetalheProduto(id);

        if (product == null)
        {
            ReportError("Only the user who created it can delete the record.");
            return CustomResponse();
        }

        return Ok(product); // sem conversão para ProdutoViewModel
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoViewModel>> Add([FromBody] ProdutoViewModel produtoViewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var completeName = $"{Guid.NewGuid()}_{produtoViewModel.Imagem}".Trim();

        var fileName = await UploadFile(produtoViewModel.ImagemUpload, completeName);

        if (string.IsNullOrEmpty(fileName))
        {
            return CustomResponse(ModelState);
        }

        produtoViewModel.Imagem = fileName;

        await _productService.AdicionaAsync(_mapper.Map<Produto>(produtoViewModel));

        return CustomResponse(produtoViewModel);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ProdutoViewModel produtoViewModel)
    {
        if (id != produtoViewModel.Id)
        {
            ReportError("The IDs provided are not the same!");
            return CustomResponse();
        }

        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var vendedorId = FuncoesGerais.PegarOIdDoUsuarioPeloToken(Request.Headers.Authorization.ToString());

        var productUpdate = await GetProdutoByID(id, vendedorId);
        if (productUpdate == null)
        {
            ReportError("Only the user who created it can make changes.");
            return CustomResponse();
        }

        var fileName = await UpdateFile(produtoViewModel.ImagemUpload, produtoViewModel.Nome, productUpdate.Imagem);

        productUpdate.Estoque = produtoViewModel.Estoque;
        productUpdate.Preco = produtoViewModel.Preco;
        productUpdate.Nome = produtoViewModel.Nome;
        productUpdate.Descricao = produtoViewModel.Descricao;
        productUpdate.Imagem = fileName;

        await _productService.AlteraAsync(_mapper.Map<Produto>(productUpdate));

        return CustomResponse(HttpStatusCode.NoContent);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ProdutoViewModel>> Delete(Guid id)
    {
        var vendedorId = FuncoesGerais.PegarOIdDoUsuarioPeloToken(Request.Headers.Authorization.ToString());

        var product = await GetProdutoByID(id, vendedorId);

        if (product == null)
        {
            ReportError("Only the user who created it can delete the record.");
            return CustomResponse();
        }

        var entity = await _productService.PegaPorIdAsync((Guid?)id);

        if (await _productService.ExcluiAsync(id))
        {
            UpdateFile(entity.Imagem);
        }

        return CustomResponse(HttpStatusCode.NoContent);
    }

    private async Task<ProdutoViewModel> GetProdutoByID(Guid id, Guid vendedorId)
    {
        return _mapper.Map<ProdutoViewModel>(await _productService.PegaPorIdAsync(id, vendedorId));
    }

    private async Task<ProdutoDetalhesViewModel> DetalheProduto(Guid id)
    {
        return _mapper.Map<ProdutoDetalhesViewModel>(await _productService.DetalheProduto(id));
    }

    private async Task<string> UploadFile(string arquivo, string imgNome)
    {
        if (string.IsNullOrEmpty(arquivo))
        {
            ReportError("Please provide an image for this product!");
            return string.Empty;
        }

        var imageName = await _imageService.SaveImageAsync(arquivo, imgNome);

        return imageName;
    }

    private async Task<string> UpdateFile(string arquivo, string imgNome, string oldFileName)
    {
        if (string.IsNullOrEmpty(arquivo))
        {
            ReportError("Please provide an image for this product!");
            return string.Empty;
        }

        var imageName = await _imageService.UpdateImageAsync(arquivo, imgNome, oldFileName);

        return imageName;
    }

    private void UpdateFile(string delete)
    {
        if (string.IsNullOrEmpty(delete))
        {
            ReportError("Please provide an image for this product!");
            return;
        }

        _imageService.DeleteFile(delete);
    }
}