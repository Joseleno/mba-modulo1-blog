
using MbaBlog.Infrastructure;

namespace MbaBlog.Mvc
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AjoutDepen(this IServiceCollection services)
        {
            services.AddScoped<IAppIdentityUser, AppIdentityUser>();

            return services;
        }
    }
}