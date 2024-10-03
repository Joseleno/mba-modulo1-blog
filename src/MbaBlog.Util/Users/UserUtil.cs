using MbaBlog.Infrastructure.Repositories.Users;
using MbaBlog.Util.Exceptions;
using MbaBlog.Util.Users.Dtos;

namespace MbaBlog.Util.Users;

public class UserUtil(IAppIdentityUser appIdentityUser, IRepositoryUserRole iUserRole, IRepositoryUser repositoryUser) : IUserUtil
{
    private readonly IAppIdentityUser _appIdentityUser = appIdentityUser;
    private readonly IRepositoryUserRole _iUserRole = iUserRole;
    private readonly IRepositoryUser _repositoryUser = repositoryUser;
    public UserDto GetUser()
    {
        var userId = _appIdentityUser.GetUserId();
        var username = _appIdentityUser.GetUsername();
        if (userId == Guid.Empty || username == null)
        {
            throw new NotFoundException("Usuario nao cadastrado");
        }

        return new UserDto() { UserId = userId, UserEmail = username };
    }

    public bool HasAthorization(Guid id)
    {
        var userId = _appIdentityUser.GetUserId();
        var user = _repositoryUser.GetUser(userId);
        if(user == null)
        {
            return false;
        }
        var role = _iUserRole.GetRole(userId);

        if ((userId == id) || (role != null && role.Equals("admin", StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }
        return false;
    }

    public bool IsUser(Guid id)
    {
        var result = _repositoryUser.GetUser(id) != null;
        return result;
    }

}
