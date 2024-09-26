
using GameShopModel.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameShop.ViewModel;

public class RecommendedGameProductViewModel
{
    public required string ExpertName { get; set; }
    public required string ExpertSurname { get; set; }
    public required string SelectedGameProduct { get; set; }
    public required string SearchGameProduct {  get; set; }
    public SelectList? GameProducts { get; set; }
}
