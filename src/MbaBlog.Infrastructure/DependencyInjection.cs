using MbaBlog.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MbaBlog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddDatabase(configuration);


    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        var SqLiteDatabaseConnectionString = configuration.GetConnectionString("SqLiteDatabase") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        //services.AddDbContext<ApplicationDbContext>(
        //    options => options.UseSqlServer(connectionString));
        
        

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(SqLiteDatabaseConnectionString));
        
        services.AddDbContext<MbaBlogDbContext>(options =>
            options.UseSqlite(SqLiteDatabaseConnectionString));
        
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }

}
