using GameShop.Controllers;
using GameShopModel.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameShop.ViewModel;

public class FilteredGameProductViewModel
{
    public string? NameSearchString { get; set; }
    public string? GameGenre {  get; set; }
    public Filter SelectFilter { get; set; }
    //public required SelectList Filter { get; set; }
    public required List<GameProduct> GameProducts { get; set; }
    public required SelectList GameGenres {  get; set; } 
}
