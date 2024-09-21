using MbaBlog.Domain.Domain;

namespace MbaBlog.Infrastructure.Repositorys.Posts
{
    public interface IRepositoryPost
    {
        Task<Post> CreatePost(string userId, Post post);
        Task<Post> EditPost(string userId, Post post);
        Task<IEnumerable<Post>> GetPostsByIdAutor(Guid userId);
        Task<Post> GetPostById(Guid postId);

    }
}
