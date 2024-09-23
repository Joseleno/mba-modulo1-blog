using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Infrastructure.Repositorys.Comentarios
{
    public class RepositoryComentario(MbaBlogDbContext myBlogContext) : IRepositoryComentario
    {
        private readonly MbaBlogDbContext _myBlogContext = myBlogContext;

        public Task<ComentarioPost> Create(ComentarioPost ComentarioPost)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ComentarioPost> Edit(ComentarioPost ComentarioPost)
        {
            throw new NotImplementedException();
        }

        public Task<ComentarioPost?> GetComentarioById(Guid ComentarioPostId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComentarioPost>> GetComentariosByIdAutor(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ComentarioPost>> GetComentariosByIdPost(Guid id)
        {
            return await _myBlogContext.Comentarios.Where(c => c.PostId == id).ToListAsync();
        }
    }
}
