using MbaBlog.Domain.Domain;

namespace MbaBlog.Infrastructure.Repositories.Posts;

public interface IRepositoryPost
{
    Task<Post> Create(Post post);

    Task<Post> Editar(Post post);

    Task<IEnumerable<Post>> GetAllByIdAutor(Guid id);

    Task<IEnumerable<Post>> GetAll();

    Task<Post?> GetById(Guid postId);

    Task Delete(Post post);

}
