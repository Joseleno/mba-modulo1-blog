using MbaBlog.Infrastructure;
using MbaBlog.Mvc.Extensions;

namespace MbaBlog.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();

            builder.Services
                .AddInfrastructure(builder.Configuration)
                .AddControllersWithViews();

            builder.Services.AdicionarRepositorio();
            builder.Services.AdicionarUtils();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            //app.MapControllerRoute(
            //    name: "comentarios",
            //    pattern: "{controller=Comentarios}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.UseDbMigrationHelper();

            app.Run();
        }
    }
}
