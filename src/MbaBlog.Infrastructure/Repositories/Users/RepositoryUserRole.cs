using MbaBlog.Infrastructure.Data;

namespace MbaBlog.Infrastructure.Repositories.Users;

public class RepositoryUserRole(ApplicationDbContext appctx) : IRepositoryUserRole
{
    private readonly ApplicationDbContext _appctx = appctx;
    public string? GetRole(Guid userId)
    {
        var role = _appctx.UserRoles.SingleOrDefault(r => r.UserId == userId.ToString());
        if (role == null)
        {
            return null;
        }
        var roles = from r in _appctx.Roles
                    join u in _appctx.UserRoles on r.Id equals u.RoleId
                    select r.Name;
        var result = _appctx.Roles.SingleOrDefault(r => r.Id == role.RoleId);

        return result?.Name;
    }
}
