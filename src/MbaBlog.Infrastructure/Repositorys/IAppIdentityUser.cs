namespace MbaBlog.Infrastructure;

public interface IAppIdentityUser
{
    string GetUsername();
    Guid GetUserId();
    bool IsAuthenticated();
    bool IsInRole(string role);
 
}
