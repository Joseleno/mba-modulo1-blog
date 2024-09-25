using MbaBlog.Infrastructure.Repositories.Users;
using MbaBlog.Utils.Users.Dtos;

namespace MbaBlog.Utils.Users;

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
            throw new Exception("Usuario nao cadastrado");
        }

        return new UserDto() { UserId = userId, UserEmail = username };
    }

    public bool HasAthorization(Guid id)
    {
       
        var usertId = _appIdentityUser.GetUserId();
        var result = usertId == id || IsAdmin(usertId);
        return result;
    }

    public bool IsUser(Guid id)
    {
        var result = _repositoryUser.GetUser(id) != null;
        return result;
    }

    private bool IsAdmin(Guid id)
    {
        var role = _iUserRole.GetRole(id);
        if (role == null)
        {
            return false;
        }
        var result = role.Equals("admin", StringComparison.OrdinalIgnoreCase);
        return result;
    }

}
