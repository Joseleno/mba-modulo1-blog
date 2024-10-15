﻿using MbaBlog.Data.Dtos;

namespace MbaBlog.Util.Users;

public interface IUserUtil
{
    UserDto GetUser();

    bool HasAthorization(Guid id);

    bool IsUser(Guid id);

}
