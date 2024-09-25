namespace MbaBlog.Infrastructure.Repositories.Users;

public interface IRepositoryUserRole
{
    string? GetRole(Guid userId);
}
