using MbaBlog.Domain.Domain;
using MbaBlog.Infrastructure;
using MbaBlog.Utils.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Utils.Users;

public class UserUtil(IAppIdentityUser appIdentityUser) : IUserUtil
{
    private readonly IAppIdentityUser _appIdentityUser = appIdentityUser;
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
        var resultId = _appIdentityUser.GetUserId();
        return resultId == id;
    }
}
