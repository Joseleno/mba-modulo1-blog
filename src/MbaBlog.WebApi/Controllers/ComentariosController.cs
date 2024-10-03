using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure.Repositories.Comentarios;
using MbaBlog.Util.Users;
using MbaBlog.WebApi.Data.Dtos;
using MbaBlog.WebApi.Data.Mappers;
using MbaBlog.Infrastructure.Repositories.Posts;
using Microsoft.AspNetCore.Authorization;

namespace MbaBlog.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ComentariosController(IRepositoryComentario repositoryComentario,
    IRepositoryPost repositoryPost, IUserUtil iUserUtil, IMapperComentario mapperComentario,
    ILogger<ComentariosController> logger) : ControllerBase
{
    private readonly IRepositoryComentario _repositoryComentario = repositoryComentario;
    private readonly IRepositoryPost _repositoryPost = repositoryPost;
    private readonly IUserUtil _iUserUtil = iUserUtil;
    private readonly IMapperComentario _mapperComentario = mapperComentario;

    private readonly ILogger<ComentariosController> _logger = logger;

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ComentarioPost?>> Get(Guid id)
    {
        var result = await _repositoryComentario.GetById(id);
        if (result is null)
        {
            _logger.LogInformation("Comentario nao encontrado - {Id}", id);
            return NotFound();
        }
        return result;
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
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

        var comentarioBd = await _repositoryComentario.GetById(id);

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
