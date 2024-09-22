using GameShop.Core;

namespace GameShop.ViewModel;

public class SortGameProductVM(SortGameProductState? sortTitleGameProduct)
{
    public SortGameProductState? SortTitleGameProduct => sortTitleGameProduct;
}
