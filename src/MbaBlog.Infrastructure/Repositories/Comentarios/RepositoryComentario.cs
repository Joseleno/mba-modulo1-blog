using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MbaBlog.Infrastructure.Repositories.Comentarios
{
    public class RepositoryComentario(MbaBlogDbContext myBlogContext) : IRepositoryComentario
    {
        private readonly MbaBlogDbContext _myBlogContext = myBlogContext;

        public async Task<ComentarioPost> Create(ComentarioPost ComentarioPost)
        {
            _myBlogContext.Comentarios.Add(ComentarioPost);
            await _myBlogContext.SaveChangesAsync();

            return ComentarioPost;
        }

        public async Task Delete(Guid id)
        {
            var comentario = await GetById(id);
            if (comentario != null)
            {
                _myBlogContext.Comentarios.Remove(comentario);
            }

            await _myBlogContext.SaveChangesAsync();

        }

        public async Task<ComentarioPost> Edit(ComentarioPost comentarioPost)
        {
            _myBlogContext.Entry(comentarioPost).State = EntityState.Detached;
            _myBlogContext.Comentarios.Update(comentarioPost);
            await _myBlogContext.SaveChangesAsync();

            return comentarioPost;
        }

        public async Task<ComentarioPost?> GetById(Guid id)
        {
            return await _myBlogContext.Comentarios.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ComentarioPost>> GetComentariosByIdPost(Guid id)
        {
            return await _myBlogContext.Comentarios.Where(c => c.PostId == id).ToListAsync();
        }
    }
}
