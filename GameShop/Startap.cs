using GameShopModel.Data;
using GameShopModel.Repositories.Interface;
using GameShopModel.Repositories;
using Microsoft.EntityFrameworkCore;
using GameShop.Repository.Interfaces;
using GameShop.Repository;

namespace GameShop;

public class Startap(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IGameProductRepository, GameProductRepository>();
        services.AddSingleton<IRepositoryCart, RepositoryCart>();
        services.AddControllersWithViews();
        services.AddDbContext<GameShopContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("GameShopContext") ??
            throw new InvalidOperationException("Connection string 'GameShopContext' not found.")));
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
        //Seed.SeedUsersAndRolesAsync(app);
    }
}
