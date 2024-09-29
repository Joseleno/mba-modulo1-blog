using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MbaBlog.Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using MbaBlog.Util.Users;
using MbaBlog.Infrastructure.Repositories.Comentarios;
using MbaBlog.Infrastructure.Repositories.Posts;

namespace MbaBlog.Mvc.Controllers;

[Authorize]
[Route("comentarios")]
public class ComentariosController(IRepositoryComentario repositoryComentario, IUserUtil iUserUtil) : Controller
{
    private readonly IRepositoryComentario _repositoryComentario = repositoryComentario;
    private readonly IUserUtil _iUserUtil = iUserUtil;

    public IActionResult Create(Guid id)
    {
        ViewData["PostId"] = id;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Comentario,Id, PostId")] ComentarioPost comentarioPost)
    {
        if (ModelState.IsValid)
        {
            var autorId = _iUserUtil.GetUser().UserId;

            comentarioPost.AutorId = autorId;

            await _repositoryComentario.Create(comentarioPost);

            return RedirectToAction(comentarioPost.PostId.ToString(), "Posts");
        }

        return View(comentarioPost);
    }

    [Route("{id:Guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var comentarioPost = await _repositoryComentario.GetById(id);
        if (comentarioPost == null)
        {
            return NotFound();
        }
        ViewData["AutorId"] = comentarioPost.AutorId;
        ViewData["ComentarioPostId"] = comentarioPost.PostId;
        return View(comentarioPost);
    }

    [HttpPost("{id:Guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Comentario,Id, AutorId, PostId")] ComentarioPost comentarioPost)
    {
        if (id != comentarioPost.Id)
        {
            return NotFound();
        }

        if (!_iUserUtil.HasAthorization(comentarioPost!.AutorId))
        {
            return RedirectToAction("Index", "Validations");
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _repositoryComentario.Edit(comentarioPost);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(comentarioPost.PostId.ToString(), "Posts");
        }

        return View(comentarioPost);
    }

    [Route("excluir/{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var comentarioPost = await _repositoryComentario.GetById(id);
        if (comentarioPost == null)
        {
            return NotFound();
        }

        return View(comentarioPost);
    }

    [HttpPost("excluir/{id:Guid}")]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var comentarioPost = await _repositoryComentario.GetById(id);
        if (comentarioPost != null)
        {
            await _repositoryComentario.Delete(id);
        }

        return RedirectToAction("Index", "Posts");
    }
}
