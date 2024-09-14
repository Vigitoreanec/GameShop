using GameShopModel.Entities;

namespace GameShop.ViewModel;

public class CartViewModel
{
    public required IEnumerable<GameProduct> GameProducts { get; init; }
    public required decimal SumGameProducts {  get; init; }
}
