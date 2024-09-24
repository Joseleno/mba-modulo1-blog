namespace MbaBlog.Infrastructure.Repositories;

public interface IAppIdentityUser
{
    string GetUsername();
    Guid GetUserId();
    bool IsAuthenticated();
    bool IsInRole(string role);

}
