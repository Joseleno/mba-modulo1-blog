using Azure;
using Humanizer;
using MbaBlog.Domain.Domain;
using MbaBlog.Mvc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Infrastructure.Services.Posts
{
    public class ServicePost : IServicePost
    {
        private readonly MbaBlogDbContext _ctx;
        private readonly ApplicationDbContext _appctx;

        public ServicePost(MbaBlogDbContext ctx, ApplicationDbContext appctx)
        {
            _ctx = ctx;
            _appctx = appctx;
        }

        public async Task<Post> CreatePost(string email, Post post)
        {
            
            //var author = await _context.Bloggers.FirstOrDefaultAsync(b => b.UserId == userId);
            var user = await _appctx.Users.FirstOrDefaultAsync(b => b.UserName!.Equals(email, StringComparison.OrdinalIgnoreCase));

            
            
            var result = new Post() { AutorId = Guid.NewGuid(), Texto = post.Texto, Titulo = post.Titulo};

            _ctx.Posts.Add(post);
            await _ctx.SaveChangesAsync();

            return result;
        }

        public Task<Post> EditPost(Guid userId, Post post)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPostById(Guid postId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetPostsByIdAutor(string email)
        {
            var result = await _ctx.Posts.Where( p => p.Autor!.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).ToListAsync();

            return result;
        }
    }
}
