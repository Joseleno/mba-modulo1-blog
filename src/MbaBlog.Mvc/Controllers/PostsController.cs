using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MbaBlog.Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using MbaBlog.Util.Users;
using MbaBlog.Infrastructure.Repositories.Posts;

namespace MbaBlog.Mvc.Controllers;

[Authorize]
[Route("[controller]")]
public class PostsController(IRepositoryPost repositoryPost, IUserUtil userUtil) : Controller
{
    private readonly IRepositoryPost _repositoryPost = repositoryPost;
    private readonly IUserUtil _userUtil = userUtil;


    [AllowAnonymous]
    public async Task<IActionResult> Index() 
    {

        return View(await _repositoryPost.GetAll());
    }

    [Route("{id:Guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var post = await _repositoryPost.GetById(id);
        if (post == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest();
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
    public async Task<IActionResult> Create([Bind("Titulo,Texto,CriadoEm")] Post post)
    {

        if (ModelState.IsValid)
        {
            post.AutorId = _userUtil.GetUser().UserId;

            if (!_userUtil.HasAthorization(post!.AutorId))
            {
                return RedirectToAction("Index", "Validations");
            }
            await _repositoryPost.Create(post);

            return RedirectToAction(nameof(Index));
        }
        return View(post);
    }

    [Route("editar/{id:Guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {

        var post = await _repositoryPost.GetById(id);
        if (!_userUtil.HasAthorization(post!.AutorId))
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
    public async Task<IActionResult> Edit(Guid id, [Bind("AutorId,Titulo,Texto,Id,CriadoEm,ModificadoEm")] Post post)
    {
        if (id != post.Id)
        {
            return NotFound();
        }

        if (!_userUtil.HasAthorization(post!.AutorId))
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
                await _repositoryPost.Editar(post);
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
        var post = await _repositoryPost.GetById(id);

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
        var post = await _repositoryPost.GetById(id);

        if (post == null)
        {
            return NotFound();
        }

        if (!_userUtil.HasAthorization(post!.AutorId))
        {
            return RedirectToAction("Index", "Validations");
        }

            await _repositoryPost.Delete(post);
        return RedirectToAction(nameof(Index));
    }

}
