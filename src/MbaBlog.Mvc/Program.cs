using MbaBlog.Infrastructure;
using MbaBlog.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;

namespace MbaBlog.Mvc;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<SecurityStampValidatorOptions>(options =>
        {
            // enables immediate logout, after updating the user's stat.
            options.ValidationInterval = TimeSpan.Zero;
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services
            .AddInfrastructure(builder.Configuration)
            .AddControllersWithViews();

        builder.Services.AdicionarRepositorio();
        builder.Services.AdicionarUtils();

        

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.UseDbMigrationHelper();

        app.Run();
    }
}