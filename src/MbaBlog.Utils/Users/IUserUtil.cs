using MbaBlog.Utils.Users.Dtos;

namespace MbaBlog.Utils.Users;

public interface IUserUtil
{
    UserDto GetUser();

    bool HasAthorization(Guid id);

    bool IsUser(Guid id);

}
