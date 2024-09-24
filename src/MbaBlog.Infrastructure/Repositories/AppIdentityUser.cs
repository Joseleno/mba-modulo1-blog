using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace MbaBlog.Infrastructure.Repositories;

public class AppIdentityUser(IHttpContextAccessor accessor) : IAppIdentityUser
{
    private readonly IHttpContextAccessor _accessor = accessor;

    public Guid GetUserId()
    {
        if (!IsAuthenticated()) return Guid.Empty;

        var claim = _accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(claim))
            claim = _accessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        return claim is null ? Guid.Empty : Guid.Parse(claim);
    }

    public string GetUsername()
    {
        var username = _accessor.HttpContext?.User.FindFirst("username")?.Value;
        if (!string.IsNullOrEmpty(username)) return username;

        username = _accessor.HttpContext?.User.Identity?.Name;
        if (!string.IsNullOrEmpty(username)) return username;

        return string.Empty;
    }

    public bool IsAuthenticated()
    {
        return _accessor.HttpContext?.User.Identity is { IsAuthenticated: true };
    }

    public bool IsInRole(string role)
    {
        return _accessor.HttpContext != null && _accessor.HttpContext.User.IsInRole(role);
    }

}
