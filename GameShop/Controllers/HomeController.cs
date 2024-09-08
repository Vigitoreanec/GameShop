using GameShopModel.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Controllers;

public class HomeController(IGameProductRepository gameProductRepository) : Controller
{
    public async Task <IActionResult> Index()
    {
        var gameProducts = await gameProductRepository.GetAllGameProductsAsync();
        return View(gameProducts);
    }

    public IActionResult PopularGames()
    {
        return View();
    }
    public IActionResult RecommendationGames()
    {
        return View();
    }
    public IActionResult WishList()
    {
        return View();
    }
}
