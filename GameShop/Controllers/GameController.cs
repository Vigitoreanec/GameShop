using GameShop.Repository.Interfaces;
using GameShopModel.Data;
using GameShopModel.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Controllers;

public class GameController(IGameProductRepository gameProductRepository, IRepositoryCart repositoryCart, GameShopContext gameShopContext) : Controller
{
    public async Task<IActionResult> Details(int id)
    {
        var gameProduct = await gameShopContext.GameProducts
            .Include(gameProduct => gameProduct.Genres)
            .Include(gameProduct => gameProduct.Images)
            .Include (gameProduct => gameProduct.Videos)
            .Include(gameProduct => gameProduct.MinimumSystemRequirements)
            .Include(gameProduct => gameProduct.RecommendedSystemRequirements)
            .FirstAsync(gameProduct => gameProduct.Id == id);
        return View(gameProduct);
    }
    public async Task<IActionResult> Add(int id)
    {
        var gameProduct = await gameProductRepository.GetGameProductAsync(id);
        repositoryCart.Add(gameProduct);
        return Redirect("~/Cart/Index");
    }
}
