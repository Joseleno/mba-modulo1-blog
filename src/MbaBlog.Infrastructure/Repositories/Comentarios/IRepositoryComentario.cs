using MbaBlog.Domain.Domain;

namespace MbaBlog.Infrastructure.Repositories.Comentarios;

public interface IRepositoryComentario
{
    Task<ComentarioPost> Create(ComentarioPost ComentarioPost);

    Task<ComentarioPost> Edit(ComentarioPost ComentarioPost);


    Task<ComentarioPost?> GetComentarioById(Guid ComentarioPostId);

    Task Delete(Guid id);
}
