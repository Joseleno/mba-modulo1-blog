using MbaBlog.Util.Users;
using MbaBlog.Infrastructure.Repositories.Users;
using MbaBlog.WebApi.Data.Mappers;
using MbaBlog.Util.Autor;
using MbaBlog.Data.Repositories.Comentarios;
using MbaBlog.Data.Repositories.Posts;
using MbaBlog.Data.Repositories.Users;

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
        services.AddScoped<IAutorUtil, AutorUtil>();

        return services;
    }
}
