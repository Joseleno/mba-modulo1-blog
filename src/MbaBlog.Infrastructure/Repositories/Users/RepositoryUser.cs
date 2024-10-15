﻿using MbaBlog.Infrastructure.Data;
using MbaBlog.Infrastructure.Dtos;

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
