
namespace GameShopModel.Entities;

public class RecommendedGameProducts
{
    public int Id { get; set; } 
    public required string ExpertName { get; set; }
    public required string ExpertSurname { get; set; }
    public required GameProduct GameProduct { get; set; }
}
