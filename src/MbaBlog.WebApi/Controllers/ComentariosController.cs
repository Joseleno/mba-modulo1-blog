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
using MbaBlog.WebApi.Data.Dtos;
using MbaBlog.WebApi.Data.Mappers;
using MbaBlog.Infrastructure.Repositories.Posts;

namespace MbaBlog.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComentariosController(IRepositoryComentario repositoryComentario, IRepositoryPost repositoryPost, IUserUtil iUserUtil, IMapperComentario mapperComentario) : Controller
{
    private readonly IRepositoryComentario _repositoryComentario = repositoryComentario;
    private readonly IRepositoryPost _repositoryPost = repositoryPost;
    private readonly IUserUtil _iUserUtil = iUserUtil;
    private readonly IMapperComentario _mapperComentario = mapperComentario;

    [HttpPost()]
    public async Task<IActionResult> Create(ComentarioDto comentarioDto)
    {
        if (comentarioDto.AutorId == Guid.Empty || !_iUserUtil.IsUser(comentarioDto.AutorId))
        {

            return ValidationProblem("Usuario nao cadastrado");
        }

        if (comentarioDto.PostId == Guid.Empty)
        {

            return ValidationProblem("PostId nao encontrado");
        }

        var post = await _repositoryPost.GetById(comentarioDto.PostId);

        if (post is null)
        {

            return ValidationProblem("Post nao cadastrado");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _repositoryComentario.Create(_mapperComentario.Mapcomentario(comentarioDto));
        return Ok(new { post.Id });
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Edit(Guid id, ComentarioDto comentario)
    {
        if (!_iUserUtil.IsUser(comentario!.AutorId))
        {
            return ValidationProblem(StatusCodes.Status400BadRequest.ToString());
        }

        var comentarioBd = await _repositoryComentario.GetById(id); ;

        if (comentarioBd!.PostId != comentario.PostId)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {

                await _repositoryComentario.Edit(_mapperComentario.Mapcomentario(comentario));
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
        var comentario = await _repositoryComentario.GetById(id);
        if (comentario != null)
        {
            await _repositoryComentario.Delete(id);
        }

        return NoContent();
    }

    private bool PostExists(Guid id)
    {
        return _repositoryComentario.GetById(id) != null;
    }
}
