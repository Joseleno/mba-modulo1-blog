using MbaBlog.Domain.Domain;

namespace MbaBlog.Infrastructure.Services.Posts
{
    public interface IServicePost
    {
        Task<Post> CreatePost(string email, Post post);
        Task<Post> EditPost(Guid userId, Post post);
        Task<IEnumerable<Post>> GetPostsByIdAutor(string email);
        Task<Post> GetPostById(Guid postId);

    }
}
