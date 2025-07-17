using AutoMapper;
using MBA.Modulo2.Api.Extensions;
using MBA.Modulo2.Api.ViewModels;
using MBA.Modulo2.Business.Functions;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace MBA.Modulo2.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComentarioController(ICommentService commentService,
                               IPostService postService,
                               IMapper mapper, 
                               INotifier notifier) : MainController(notifier)
{
    private readonly ICommentService _commentService = commentService;
    private readonly IPostService _postService = postService;
    private readonly IMapper _mapper = mapper;

    [HttpGet]    
    public async Task<IActionResult> GetAll()
    {
        var sellerId = GeneralFunctions.GetUserIdFromToken(Request.Headers.Authorization.ToString());

        var comments = await _commentService.GetAllAsync(sellerId);

        var viewModel = _mapper.Map<IEnumerable<ComentarioViewModel>>(comments);
        return Ok(viewModel);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var comment = await _commentService.GetByIdAsync(id);
        if (comment == null)
        {
            ReportError("This comment does not exist.");
            return CustomResponse();
        }

        var viewModel = _mapper.Map<ComentarioViewModel>(comment);
        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ComentarioViewModel commentViewModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var post = await _postService.GetByIdAsync(commentViewModel.PostId);
        if (post == null)
        {
            ReportError("The comment post ID does not exist, please insert an existing post");
            return CustomResponse();
        }

        var comment = _mapper.Map<Comentario>(commentViewModel);
        await _commentService.AddAsync(comment);
        return CreatedAtAction(nameof(GetById), new { id = comment.Id }, commentViewModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ComentarioViewModel commentViewModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != commentViewModel.Id) return BadRequest("Mismatched comment ID");

        var post = await _postService.GetByIdAsync(commentViewModel.PostId);
        if (post == null)
        {
            ReportError("This post does not exist");
            return CustomResponse();
        }

        var commentValidate = await _commentService.GetByIdAsync(id);
        if (commentValidate == null)
        {
            ReportError("This comment does not exist.");
            return CustomResponse();
        }

        var comment = _mapper.Map<Comentario>(commentViewModel);

        var sellerId = GeneralFunctions.GetUserIdFromToken(Request.Headers.Authorization.ToString());

        await _commentService.UpdateAsync(comment, sellerId);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var commentValidate = await _commentService.GetByIdAsync(id);
        if (commentValidate == null)
        {
            ReportError("This comment does not exist.");
            return CustomResponse();
        }

        var sellerId = GeneralFunctions.GetUserIdFromToken(Request.Headers.Authorization.ToString());

        await _commentService.DeleteAsync(id, sellerId);
        return NoContent();
    }
}