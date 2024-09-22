using MbaBlog.Infrastructure;
using MbaBlog.Infrastructure.Repositorys.UserRole;
using MbaBlog.Utils.Users.Dtos;

namespace MbaBlog.Utils.Users;

public class UserUtil(IAppIdentityUser appIdentityUser, IRepositoryUserRole iUserRole) : IUserUtil
{
    private readonly IAppIdentityUser _appIdentityUser = appIdentityUser;
    private readonly IRepositoryUserRole _iUserRole = iUserRole;
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
