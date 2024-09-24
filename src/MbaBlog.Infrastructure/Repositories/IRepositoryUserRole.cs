namespace MbaBlog.Infrastructure.Repositorys.UserRole;

public interface IRepositoryUserRole
{
    string? GetRole(Guid userId);
}
