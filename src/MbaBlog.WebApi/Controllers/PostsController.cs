using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure.Repositories.Posts;
using MbaBlog.Util.Users;

namespace MbaBlog.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController(IRepositoryPost repositoryPost, IUserUtil iUserUtil) : Controller
{
    private readonly IRepositoryPost _repositoryPost = repositoryPost;
    private readonly IUserUtil _iUserUtil = iUserUtil;

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
    public async Task<IActionResult> Create([FromBody] Post post)
    {
        if (post.AutorId == Guid.Empty || !_iUserUtil.IsUser(post.AutorId))
        {

            return ValidationProblem("Usuario nao cadastrado");
        }
        
        if (!ModelState.IsValid)
        {
            
            return BadRequest(ModelState);
        }

        post.CriadoEm = DateTime.Now;
        await _repositoryPost.CreatePost(post);

        return Ok(new {post});
    }

    //// GET: Posts/Edit/5
    //public async Task<IActionResult> Edit(Guid? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var post = await _context.Posts.FindAsync(id);
    //    if (post == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(post);
    //}

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
