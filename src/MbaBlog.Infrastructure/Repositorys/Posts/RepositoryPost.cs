using Azure;
using Humanizer;
using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure.Data;
using MbaBlog.Mvc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Infrastructure.Repositorys.Posts;

public class RepositoryPost(MbaBlogDbContext ctx, ApplicationDbContext appctx) : IRepositoryPost
{
    private readonly MbaBlogDbContext _ctx = ctx;
    private readonly ApplicationDbContext _appctx = appctx;

    public async Task<Post> CreatePost(string userId, Post post)
    {
        
        //var author = await _context.Bloggers.FirstOrDefaultAsync(b => b.UserId == userId);
        var user = await _appctx.Users.FirstOrDefaultAsync(b => b.Id == userId);

        
        
        var result = new Post() { AutorId = userId, Texto = post.Texto, Titulo = post.Titulo};

        _ctx.Posts.Add(post);
        await _ctx.SaveChangesAsync();

        return result;
    }

    public Task<Post> EditPost(string userId, Post post)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetPostById(Guid postId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Post>> GetPostsByIdAutor(Guid userId)
    {
        var result = await _ctx.Posts.Where( p => p.AutorId.Equals(userId.ToString(), StringComparison.CurrentCultureIgnoreCase)).ToListAsync();

        return result;
    }
}
