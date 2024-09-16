using GameShopModel.Entities;

namespace GameShop.ViewModel;

public class WishGameProductViewModel
{
    public required GameProduct GameProduct { get; set; }
    public bool ContainsWishGameProduct { get; set; }
    public bool ContainsGameProduct { get; set; }
}
