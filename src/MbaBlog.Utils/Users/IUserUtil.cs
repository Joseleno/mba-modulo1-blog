using MbaBlog.Utils.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Utils.Users;

public interface IUserUtil
{
    UserDto GetUser();
    bool HasAthorization(Guid id);
}
