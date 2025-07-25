﻿using AutoMapper;
using MBA.Modulo2.Api.ViewModels;
using MBA.Modulo2.Business.Functions;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBA.Modulo2.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class PostController(IPostService postService,
                            IMapper mapper,
                            INotifier notifier) : MainController(notifier)
{
    private readonly IPostService _postService = postService;
    private readonly IMapper _mapper = mapper;

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostViewModel>>> GetAll()
    {
        var vendedorId = FuncoesGerais.PegarOIdDoUsuarioPeloToken(Request.Headers.Authorization.ToString());

        var postViewModels = _mapper.Map<IEnumerable<PostViewModel>>(await _postService.PegarTodosAsync(vendedorId));

        return Ok(postViewModels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostViewModel>> GetById(Guid id)
    {
        var post = await _postService.PegarPorIdAsync(id);
        if (post == null)
        {
            ReportError("This post does not exist");
            return CustomResponse();
        }

        var postViewModel = _mapper.Map<PostViewModel>(post);
        return Ok(postViewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<PostViewModel>> Create([FromBody] PostViewModel postViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var vendedorId = FuncoesGerais.PegarOIdDoUsuarioPeloToken(Request.Headers.Authorization.ToString());

        var post = _mapper.Map<Post>(postViewModel);
        post.VendedorId = vendedorId;
        post.CriadoEm = DateTime.UtcNow;

        await _postService.AdicionaAsync(post);

        var createdPostViewModel = _mapper.Map<PostViewModel>(post);

        return CreatedAtAction(nameof(GetById), new { id = post.Id }, createdPostViewModel);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] PostViewModel postViewModel)
    {
        if (id != postViewModel.Id)
        {
            return BadRequest();
        }

        // Get vendedorId  from the logged-in user
        var vendedorId = FuncoesGerais.PegarOIdDoUsuarioPeloToken(Request.Headers.Authorization.ToString());
        postViewModel.VendedorId = vendedorId;
        var post = _mapper.Map<Post>(postViewModel);


        var postUpdate = await _postService.PegarPorIdAsync(id);
        if (postUpdate == null)
        {
            ReportError("Este post não existe.");
            return CustomResponse();
        }

        await _postService.AlteraAsync(post, vendedorId);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var vendedorId = FuncoesGerais.PegarOIdDoUsuarioPeloToken(Request.Headers.Authorization.ToString());

        var postUpdate = await _postService.PegarPorIdAsync(id);
        if (postUpdate == null)
        {
            ReportError("Este post não existe.");
            return CustomResponse();
        }

        await _postService.ExcluiAsync(id, vendedorId);

        return NoContent();
    }
}