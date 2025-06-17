using AutoMapper;
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
        var sellerId = GeneralFunctions.GetUserIdFromToken(Request.Headers.Authorization.ToString());

        var postViewModels = _mapper.Map<IEnumerable<PostViewModel>>(await _postService.GetAllAsync(sellerId));

        return Ok(postViewModels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostViewModel>> GetById(Guid id)
    {
        var post = await _postService.GetByIdAsync(id);
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
        var sellerId = GeneralFunctions.GetUserIdFromToken(Request.Headers.Authorization.ToString());

        var post = _mapper.Map<Post>(postViewModel);
        post.SellerId = sellerId;
        post.CreatedAt = DateTime.UtcNow;

        await _postService.AddAsync(post);

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

        // Get sellerId from the logged-in user
        var sellerId = GeneralFunctions.GetUserIdFromToken(Request.Headers.Authorization.ToString());
        postViewModel.SellerId = sellerId;
        var post = _mapper.Map<Post>(postViewModel);


        var postUpdate = await _postService.GetByIdAsync(id);
        if (postUpdate == null)
        {
            ReportError("This post does not exist.");
            return CustomResponse();
        }

        await _postService.UpdateAsync(post, sellerId);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var sellerId = GeneralFunctions.GetUserIdFromToken(Request.Headers.Authorization.ToString());

        var postUpdate = await _postService.GetByIdAsync(id);
        if (postUpdate == null)
        {
            ReportError("This post does not exist.");
            return CustomResponse();
        }

        await _postService.DeleteAsync(id, sellerId);

        return NoContent();
    }
}