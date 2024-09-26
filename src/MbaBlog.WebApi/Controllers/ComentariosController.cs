using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure.Data;
using MbaBlog.Infrastructure.Repositories.Comentarios;
using MbaBlog.Util.Users;

namespace MbaBlog.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComentariosController(IRepositoryComentario repositoryComentario, IUserUtil iUserUtil) : Controller
{
    private readonly IRepositoryComentario _repositoryComentario = repositoryComentario;
    private readonly IUserUtil _iUserUtil = iUserUtil;


  

    
    [HttpPost]
    public async Task<IActionResult> Create(ComentarioPost comentarioPost)
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

    //// GET: Comentarios/Edit/5
    //public async Task<IActionResult> Edit(Guid? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var comentarioPost = await _context.Comentarios.FindAsync(id);
    //    if (comentarioPost == null)
    //    {
    //        return NotFound();
    //    }
    //    ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Texto", comentarioPost.PostId);
    //    return View(comentarioPost);
    //}

    //// POST: Comentarios/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(Guid id, [Bind("Comentario,Id")] ComentarioPost comentarioPost)
    //{
    //    if (id != comentarioPost.Id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(comentarioPost);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ComentarioPostExists(comentarioPost.Id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Texto", comentarioPost.PostId);
    //    return View(comentarioPost);
    //}

    //// GET: Comentarios/Delete/5
    //public async Task<IActionResult> Delete(Guid? id)
    //{
    //    if (id == null)
    //    {
    //        return NotFound();
    //    }

    //    var comentarioPost = await _context.Comentarios
    //        .Include(c => c.Post)
    //        .FirstOrDefaultAsync(m => m.Id == id);
    //    if (comentarioPost == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(comentarioPost);
    //}

    //// POST: Comentarios/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(Guid id)
    //{
    //    var comentarioPost = await _context.Comentarios.FindAsync(id);
    //    if (comentarioPost != null)
    //    {
    //        _context.Comentarios.Remove(comentarioPost);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //private bool ComentarioPostExists(Guid id)
    //{
    //    return _context.Comentarios.Any(e => e.Id == id);
    //}
}
