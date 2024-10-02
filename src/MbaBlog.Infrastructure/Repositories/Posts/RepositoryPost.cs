using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MbaBlog.Infrastructure.Repositories.Posts;

public class RepositoryPost(MbaBlogDbContext myBlogContext) : IRepositoryPost
{
    private readonly MbaBlogDbContext _myBlogContext = myBlogContext;

    public async Task<Post> Create(Post post)
    {
        _myBlogContext.Posts.Add(post);
        await _myBlogContext.SaveChangesAsync();

        return post;
    }

    public async Task Delete(Post post)
    {
        _myBlogContext.Posts.Remove(post);
        await _myBlogContext.SaveChangesAsync();
    }

    public async Task<Post> Editar(Post post)
    {
        _myBlogContext.Posts.Update(post);
        await _myBlogContext.SaveChangesAsync();

        return post;
    }

    public async Task<Post?> GetById(Guid postId)
    {
        if (_myBlogContext.Posts == null)
        {
            return null;
        }
        return await _myBlogContext.Posts.Include(x => x.Comentarios).FirstOrDefaultAsync(p => p.Id == postId);
    }

    public async Task<IEnumerable<Post>> GetAll(bool? incluirComentario = false)
    {
        if (incluirComentario ?? false)
        {
            return await _myBlogContext.Posts.Include(x => x.Comentarios).OrderByDescending(p => p.CriadoEm).ToListAsync();
        }
        return await _myBlogContext.Posts.OrderByDescending(p => p.CriadoEm).ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetAllByIdAutor(Guid userId)
    {
        var result = await _myBlogContext.Posts.Where(p => p.AutorId.Equals(userId)).ToListAsync();

        return result;
    }
}
