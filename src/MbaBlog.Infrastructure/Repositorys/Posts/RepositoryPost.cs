using Azure;
using Humanizer;
using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure.Data;
using MbaBlog.Mvc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Infrastructure.Repositorys.Posts;

public class RepositoryPost(MbaBlogDbContext myBlogContext) : IRepositoryPost
{
    private readonly MbaBlogDbContext _myBlogContext = myBlogContext;

    public async Task<Post> CreatePost(Post post)
    {
        _myBlogContext.Posts.Add(post);
        await _myBlogContext.SaveChangesAsync();

        return post;
    }

    public async Task Delete(Guid id)
    {
        var post = await GetPostById(id);
        if (post != null)
        {
            _myBlogContext.Remove(post);
        }

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
        return await _myBlogContext.Posts.ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPostsByIdAutor(Guid userId)
    {
        var result = await _myBlogContext.Posts.Where( p => p.AutorId.Equals(userId)).ToListAsync();

        return result;
    }
}
