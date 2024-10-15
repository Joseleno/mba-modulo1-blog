using MbaBlog.Data.Domain;

namespace MbaBlog.Data.Repositories.Posts;

public interface IRepositoryPost
{
    Task<Post> Create(Post post);

    Task<Post> Editar(Post post);

    Task<IEnumerable<Post>> GetAllByIdAutor(Guid id);

    Task<IEnumerable<Post>> GetAll(bool? incluirComentario = false);

    Task<Post?> GetById(Guid postId);

    Task Delete(Post post);

}
