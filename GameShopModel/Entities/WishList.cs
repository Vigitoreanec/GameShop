namespace GameShopModel.Entities;

public class WishList
{
    public int Id { get; set; }
    public required User User { get; set; }
    public required GameProduct GameProduct { get; set; }
}
