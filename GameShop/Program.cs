using GameShopModel.Data;
using GameShopModel.Repositories;
using GameShopModel.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IGameProductRepository, GameProductRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GameShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameShopContext") ??
    throw new InvalidOperationException("Connection string 'GameShopContext' not found.")));

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

