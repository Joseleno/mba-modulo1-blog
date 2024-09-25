using MbaBlog.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Infrastructure.Repositories.Users
{
    public interface IRepositoryUser
    {
        UserDto? GetUser(Guid id);
    }
}
