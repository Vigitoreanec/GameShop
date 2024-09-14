using GameShopModel.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameShopModel.Data;

public class GameShopContext(DbContextOptions<GameShopContext> options) : IdentityDbContext(options)
{
    public new DbSet<User> Users {  get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<GameProduct> GameProducts {  get; set; }
    public DbSet<WishList> WishLists { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<MinimumSystemRequirement> MinimumSystemRequirements { get; set; }
    public DbSet<RecommendedSystemRequirement> RecommendedSystemRequirements { get; set; }
}
