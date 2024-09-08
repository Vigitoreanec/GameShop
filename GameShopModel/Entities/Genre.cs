
namespace GameShopModel.Entities;

public class Genre
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required List<GameProduct> GameProducts { get; set; }
}
