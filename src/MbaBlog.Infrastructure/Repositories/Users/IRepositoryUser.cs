using MbaBlog.Infrastructure.Dtos;

namespace MbaBlog.Infrastructure.Repositories.Users
{
    public interface IRepositoryUser
    {
        UserDto? GetUser(Guid id);
    }
}
