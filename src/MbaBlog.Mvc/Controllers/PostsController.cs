using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure.Data;
using MbaBlog.Infrastructure.Repositorys.Posts;
using MbaBlog.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using MbaBlog.Utils.Users;

namespace MbaBlog.Mvc.Controllers;

public class PostsController(IRepositoryPost repositoryPost, IUserUtil IUserUtil) : Controller
{
    private readonly IRepositoryPost _repositoryPost = repositoryPost;
    private readonly IUserUtil _iUserUtil = IUserUtil;
    

    // GET: Posts
    public async Task<IActionResult> Index()
    {
        return View(await _repositoryPost.GetPosts());
    }

    // GET: Posts/Details/5
    public async Task<IActionResult> Details(Guid id)
    {
        var post = await _repositoryPost.GetPostById(id);
        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    // GET: Posts/Create
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Posts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
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

    // GET: Posts/Edit/5
    [Authorize]
    public async Task<IActionResult> Edit(Guid id)
    {
        var post = await _repositoryPost.GetPostById(id);
        if (!_iUserUtil.HasAthorization(post!.AutorId))
        {
            return Content("Ediçao nao autorizada!");
        }

        if (post == null)
        {
            return NotFound();
        }
        return View(post);
    }

    // POST: Posts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("AutorId,Titulo,Texto,Id")] Post post)
    {
        if (!_iUserUtil.HasAthorization(post!.AutorId))
        {
            return Content("Ediçao nao autorizada!");
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

    //GET: Posts/Delete/5
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var post = await _repositoryPost.GetPostById(id);
        if (post == null)
        {
            return NotFound();
        }
        return View(post);
    }

    // POST: Posts/Delete/5
    [Authorize]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _repositoryPost.Delete(id);
        return RedirectToAction(nameof(Index));
    }
    
}
