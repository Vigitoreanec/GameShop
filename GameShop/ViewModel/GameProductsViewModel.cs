using GameShop.Core;
using GameShopModel.Entities;

namespace GameShop.ViewModel;

public class GameProductsViewModel
{
    public required PaginationList<GameProduct> GameProducts { get; set; }
    //public required PageViewModel PageViewModel { get; set; }
    public required FilteredGameProductVM FilteredGameProductVM { get; set; }
    public required SortGameProductVM SortGameProductVM { get; set; }
}
