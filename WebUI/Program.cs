using Domain.Configurations;
using Microsoft.EntityFrameworkCore;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseServiceProviderFactory(new IoCFactory());

            builder.Services.AddControllersWithViews();

            builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);

            builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);

            builder.Services.AddDbContext<DbContext>(cfg =>
            {
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"), opt =>
                {
                    opt.MigrationsHistoryTable("MigrationsHistory");
                });
            });

            builder.Services.Configure<EmailConfiguration>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));
            builder.Services.Configure<CryptoServiceConfiguration>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();
            app.UseStaticFiles();

            app.MapControllerRoute(name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=index}/{id?}");

            app.MapControllerRoute(name: "default",
                pattern: "{controller=home}/{action=index}/{id?}");

            app.Run();
        }
    }
}
