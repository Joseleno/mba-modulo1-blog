using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MbaBlog.Infrastructure.Repositories.Posts;

public class RepositoryPost(MbaBlogDbContext myBlogContext) : IRepositoryPost
{
    private readonly MbaBlogDbContext _myBlogContext = myBlogContext;

    public async Task<Post> CreatePost(Post post)
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

    public async Task<Post> EditPost(Post post)
    {
        _myBlogContext.Posts.Update(post);
        await _myBlogContext.SaveChangesAsync();

        return post;
    }

    public async Task<Post?> GetPostById(Guid postId)
    {
        return await _myBlogContext.Posts.Include(x => x.Comentarios).SingleOrDefaultAsync(p => p.Id == postId);
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        return await _myBlogContext.Posts.OrderByDescending(p => p.CriadoEm).ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPostsByIdAutor(Guid userId)
    {
        var result = await _myBlogContext.Posts.Where(p => p.AutorId.Equals(userId)).ToListAsync();

        return result;
    }
}
