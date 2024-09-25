using MbaBlog.Infrastructure.Dtos;
using MbaBlog.Mvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Infrastructure.Repositories.Users;

public class RepositoryUser(ApplicationDbContext applicationDbContext) : IRepositoryUser
{
    private readonly ApplicationDbContext _dbContext = applicationDbContext;

    public UserDto? GetUser(Guid id)
    {
        var query = from user in _dbContext.Users
                    where user.Id == id.ToString()
                    select new UserDto { UserId = Guid.Parse(user.Id), UserName = user.UserName};
        return query.FirstOrDefault();
    }
}
