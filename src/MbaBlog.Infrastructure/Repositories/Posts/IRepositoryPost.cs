using MbaBlog.Domain.Domain;

namespace MbaBlog.Infrastructure.Repositories.Posts;

public interface IRepositoryPost
{
    Task<Post> CreatePost(Post post);

    Task<Post> EditPost(Post post);

    Task<IEnumerable<Post>> GetPostsByIdAutor(Guid id);

    Task<IEnumerable<Post>> GetPosts();

    Task<Post?> GetPostById(Guid postId);

    Task Delete(Guid id);

}
