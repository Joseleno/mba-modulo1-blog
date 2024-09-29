using MbaBlog.Infrastructure;
using MbaBlog.Mvc.Extensions;

namespace MbaBlog.Mvc;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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