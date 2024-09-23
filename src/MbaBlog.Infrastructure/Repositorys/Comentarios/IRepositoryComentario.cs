using MbaBlog.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Infrastructure.Repositorys.Comentarios;

public interface IRepositoryComentario
{
    Task<ComentarioPost> Create(ComentarioPost ComentarioPost);

    Task<ComentarioPost> Edit(ComentarioPost ComentarioPost);

    Task<IEnumerable<ComentarioPost>> GetComentariosByIdAutor(Guid id);

    Task<IEnumerable<ComentarioPost>> GetComentariosByIdPost(Guid id);

    Task<ComentarioPost?> GetComentarioById(Guid ComentarioPostId);

    Task Delete(Guid id);
}
