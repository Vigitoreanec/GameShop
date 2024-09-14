using GameShopModel.Entities;

namespace GameShop.ViewModel;

public class WishGameProduct
{
    public required GameProduct GameProduct { get; set; }
    public bool ContainsWishGameProduct { get; set; }
    public bool ContainsGameProduct { get; set; }
}
