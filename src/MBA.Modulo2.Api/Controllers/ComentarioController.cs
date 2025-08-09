using AutoMapper;
using MBA.Modulo2.Api.ViewModels;
using MBA.Modulo2.Business.Functions;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace MBA.Modulo2.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComentarioController(IComentarioService comentatiotService,
                               IPostService postService,
                               IMapper mapper, 
                               INotifier notifier) : MainController(notifier)
{
    private readonly IComentarioService _commentService = comentatiotService;
    private readonly IPostService _postService = postService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]    
    public async Task<IActionResult> GetAll()
    {
        var vendedorId = FuncoesGerais.PegarOIdDoUsuarioPeloToken(Request.Headers.Authorization.ToString());

        var comments = await _commentService.PegarTodosAsync(vendedorId);

        var viewModel = _mapper.Map<IEnumerable<ComentarioViewModel>>(comments);
        return Ok(viewModel);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var comment = await _commentService.PegarPorIdAsync(id);
        if (comment == null)
        {
            ReportError("Este comentário não existe.");
            return CustomResponse();
        }

        var viewModel = _mapper.Map<ComentarioViewModel>(comment);
        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ComentarioViewModel commentViewModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var post = await _postService.PegarPorIdAsync(commentViewModel.PostId);
        if (post == null)
        {
            ReportError("O ID do comentário não existe, insira um post existente");
            return CustomResponse();
        }

        var comment = _mapper.Map<Comentario>(commentViewModel);
        await _commentService.AdicionaAsync(comment);
        return CreatedAtAction(nameof(GetById), new { id = comment.Id }, commentViewModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ComentarioViewModel commentViewModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != commentViewModel.Id) return BadRequest("ID de comentário incompatível");

        var post = await _postService.PegarPorIdAsync(commentViewModel.PostId);
        if (post == null)
        {
            ReportError("Esta postagem não existe");
            return CustomResponse();
        }

        var commentValidate = await _commentService.PegarPorIdAsync(id);
        if (commentValidate == null)
        {
            ReportError("Este comentário não existe.");
            return CustomResponse();
        }

        var comment = _mapper.Map<Comentario>(commentViewModel);

        var vendedorId = FuncoesGerais.PegarOIdDoUsuarioPeloToken(Request.Headers.Authorization.ToString());

        await _commentService.AlteraAsync(comment, vendedorId);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var commentValidate = await _commentService.PegarPorIdAsync(id);
        if (commentValidate == null)
        {
            ReportError("Este comentário não existe.");
            return CustomResponse();
        }

        var vendedorId = FuncoesGerais.PegarOIdDoUsuarioPeloToken(Request.Headers.Authorization.ToString());

        await _commentService.ExcluiAsync(id, vendedorId);
        return NoContent();
    }
}