using MbaBlog.Data.Domain;

namespace MbaBlog.Data.Repositories.Comentarios;

public interface IRepositoryComentario
{
    Task<ComentarioPost> Create(ComentarioPost ComentarioPost);

    Task<ComentarioPost> Edit(ComentarioPost ComentarioPost);


    Task<ComentarioPost?> GetById(Guid ComentarioPostId);

    Task Delete(Guid id);
}
