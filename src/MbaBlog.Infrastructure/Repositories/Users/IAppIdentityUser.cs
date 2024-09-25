namespace MbaBlog.Infrastructure.Repositories.Users;

public interface IAppIdentityUser
{
    string GetUsername();
    Guid GetUserId();
    bool IsAuthenticated();
    bool IsInRole(string role);

}
