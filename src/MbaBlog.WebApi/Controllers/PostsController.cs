using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure.Repositories.Posts;
using MbaBlog.Util.Users;
using MbaBlog.WebApi.Data.Dtos;
using MbaBlog.WebApi.Data.Mappers;

namespace MbaBlog.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController(IRepositoryPost repositoryPost, IUserUtil iUserUtil, IMapperPostDto mapperDto) : Controller
{
    private readonly IRepositoryPost _repositoryPost = repositoryPost;
    private readonly IUserUtil _iUserUtil = iUserUtil;
    private readonly IMapperPostDto _mapperDto = mapperDto;

    [HttpGet()]
    [Produces("application/json")]
    public async Task<IEnumerable<Post>> Get()
    {
        return await _repositoryPost.GetPosts();
    }

    [HttpGet("{id:Guid}")]
    [Produces("application/json")]
    public async Task<Post?> Get(Guid id)
    {
        return await _repositoryPost.GetPostById(id);
    }
    
    [HttpPost()]
    public async Task<IActionResult> Create(PostDto postDto)
    {
        if (postDto.AutorId == Guid.Empty || !_iUserUtil.IsUser(postDto.AutorId))
        {

            return ValidationProblem("Usuario nao cadastrado");
        }
        
        if (!ModelState.IsValid)
        {
            
            return BadRequest(ModelState);
        }
        
        var post = await _repositoryPost.CreatePost(_mapperDto.MapPost(postDto));

        return Ok(new { post.Id});
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Edit(Guid id, Post post)
    {
        if (id != post.Id)
        {
            return BadRequest();
        }

        if (!_iUserUtil.IsUser(post!.AutorId))
        {
            return ValidationProblem(StatusCodes.Status400BadRequest.ToString());
        }

        if (id != post.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _repositoryPost.EditPost(post);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        return NoContent();
    }



    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(Guid id)
    {
        var post = await _repositoryPost.GetPostById(id);
        if (post != null)
        {
            await _repositoryPost.Delete(post);
        }

        return NoContent();
    }

    private bool PostExists(Guid id)
    {
        return _repositoryPost.GetPostById(id) != null;
    }
}
