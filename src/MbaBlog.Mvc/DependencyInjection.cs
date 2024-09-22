
using MbaBlog.Infrastructure;
using MbaBlog.Infrastructure.Repositorys.Posts;
using MbaBlog.Utils.Users;

namespace MbaBlog.Mvc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AdicionarRepositorio(this IServiceCollection services)
        {
            services.AddScoped<IAppIdentityUser, AppIdentityUser>();
            services.AddScoped<IRepositoryPost, RepositoryPost>();

            return services;
        }

        public static IServiceCollection AdicionarUtils(this IServiceCollection services)
        {
            services.AddScoped<IUserUtil, UserUtil>();

            return services;
        }
    }
}