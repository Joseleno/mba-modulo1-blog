using MbaBlog.Data.Repositories.Comentarios;
using MbaBlog.Data.Repositories.Posts;
using MbaBlog.Data.Repositories.Users;
using MbaBlog.Infrastructure.Repositories.Users;
using MbaBlog.Util.Autor;
using MbaBlog.Util.Users;

namespace MbaBlog.Mvc;

public static class DependencyInjection
{
    public static IServiceCollection AdicionarRepositorio(this IServiceCollection services)
    {
        services.AddScoped<IAppIdentityUser, AppIdentityUser>();
        services.AddScoped<IRepositoryPost, RepositoryPost>();
        services.AddScoped<IRepositoryComentario, RepositoryComentario>();
        services.AddScoped<IRepositoryUserRole, RepositoryUserRole>();
        services.AddScoped<IRepositoryUser, RepositoryUser>();

        return services;
    }

    public static IServiceCollection AdicionarUtils(this IServiceCollection services)
    {
        services.AddScoped<IUserUtil, UserUtil>();
        services.AddScoped<IAutorUtil, AutorUtil>();

        return services;
    }
}