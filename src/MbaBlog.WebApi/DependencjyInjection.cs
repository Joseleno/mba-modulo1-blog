using MbaBlog.Infrastructure.Repositories.Comentarios;
using MbaBlog.Infrastructure.Repositories.Posts;
using MbaBlog.Util.Users;
using MbaBlog.Infrastructure.Repositories.Users;
using MbaBlog.WebApi.Data.Mappers;

namespace MbaBlog.WebApi;

public static class DependencjyInjection
{
    public static IServiceCollection AdicionarRepositorio(this IServiceCollection services)
    {
        services.AddScoped<IAppIdentityUser, AppIdentityUser>();
        services.AddScoped<IRepositoryPost, RepositoryPost>();
        services.AddScoped<IRepositoryComentario, RepositoryComentario>();
        services.AddScoped<IRepositoryUserRole, RepositoryUserRole>();
        services.AddScoped<IRepositoryUser, RepositoryUser>();
        services.AddScoped<IMapperPostDto, MapperPostDto>();
        services.AddScoped<IMapperComentario, MapperComentarioDto>();

        return services;
    }

    public static IServiceCollection AdicionarUtils(this IServiceCollection services)
    {
        services.AddScoped<IUserUtil, UserUtil>();

        return services;
    }
}
