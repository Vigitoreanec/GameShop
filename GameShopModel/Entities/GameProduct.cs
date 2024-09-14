
namespace GameShopModel.Entities;

public class GameProduct
{
    public int Id { get; set; }
    public required string PresentationImageUrl { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required DateTime ReleaseDate { get; set; }
    public required List<Genre>? Genres { get; set; }
    public required List<Image> Images { get; set; }
    public required List<Video> Videos { get; set; }
    public required MinimumSystemRequirement? MinimumSystemRequirements { get; set; }
    public required RecommendedSystemRequirement? RecommendedSystemRequirements { get; set; }
    public required List<Cart> Carts { get; set; }
}
