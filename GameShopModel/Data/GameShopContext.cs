using GameShopModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShopModel.Data;

public class GameShopContext(DbContextOptions<GameShopContext> options) : DbContext(options)
{
    public DbSet<GameProduct> GameProducts {  get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<ImageUrl> ImageUrls { get; set; }
}
