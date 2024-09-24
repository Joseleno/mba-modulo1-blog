using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MbaBlog.Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using MbaBlog.Utils.Users;
using MbaBlog.Infrastructure.Repositories.Posts;

namespace MbaBlog.Mvc.Controllers;

[Authorize]
[Route("posts")]
public class PostsController(IRepositoryPost repositoryPost, IUserUtil iUserUtil) : Controller
{
    private readonly IRepositoryPost _repositoryPost = repositoryPost;
    private readonly IUserUtil _iUserUtil = iUserUtil;


    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await _repositoryPost.GetPosts());
    }

    [Route("/detalhes/{id:Guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var post = await _repositoryPost.GetPostById(id);
        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    [Route("novo")]
    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost("novo")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Titulo,Texto")] Post post)
    {
        if (ModelState.IsValid)
        {
            post.AutorId = _iUserUtil.GetUser().UserId;
            await _repositoryPost.CreatePost(post);

            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }

    [Route("editar/{id:Guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {

        var post = await _repositoryPost.GetPostById(id);
        if (!_iUserUtil.HasAthorization(post!.AutorId))
        {
            return RedirectToAction("Index", "Validations");
        }

        if (post == null)
        {
            return NotFound();
        }
        return View(post);
    }

    [HttpPost("editar/{id:Guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("AutorId,Titulo,Texto,Id")] Post post)
    {
        if (id != post.Id)
        {
            return NotFound();
        }

        if (!_iUserUtil.HasAthorization(post!.AutorId))
        {
            return RedirectToAction("Index", "Validations");
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
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(post);

    }

    [Route("excluir/{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var post = await _repositoryPost.GetPostById(id);
        if (!_iUserUtil.HasAthorization(post!.AutorId))
        {
            return RedirectToAction("Index", "Validations");
        }

        if (post == null)
        {
            return NotFound();
        }
        return View(post);
    }

    [HttpPost("excluir/{id:Guid}")]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _repositoryPost.Delete(id);
        return RedirectToAction(nameof(Index));
    }
    
}
