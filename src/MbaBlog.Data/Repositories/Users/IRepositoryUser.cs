using MbaBlog.Data.Dtos;

namespace MbaBlog.Infrastructure.Repositories.Users
{
    public interface IRepositoryUser
    {
        UserDto? GetUser(Guid id);
    }
}
