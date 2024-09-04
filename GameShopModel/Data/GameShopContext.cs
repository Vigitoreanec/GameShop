using GameShopModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShopModel.Data;

public class GameShopContext(DbContextOptions<GameShopContext> options) : DbContext(options)
{
    DbSet<GameProduct> GameProducts {  get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<ImageUrl> ImageUrls { get; set; }
}
