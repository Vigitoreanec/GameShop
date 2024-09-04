using GameShopModel.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameShopContext") ??
    throw new InvalidCastException("Connection string 'GameShopContext' not found.")));
var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();

