using GameShop.Repository;
using GameShopModel.Data;
using GameShopModel.Entities;
using GameShopModel.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameShop.Controllers;

public class HomeController(GameShopContext gameShopContext, IGameProductRepository gameProductRepository, IHttpContextAccessor httpContextAccessor) : Controller
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
    public async Task<IActionResult> WishList()
    {
        var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        var wishList = await gameShopContext.WishLists
            .Include(wishList =>  wishList.User)
            .Include(wishList => wishList.GameProduct)
            .Where(wishList => wishList.User.Id == idUser)
            .ToListAsync();

        return View(wishList);
    }
    public async Task<IActionResult> DeleteWishList(int id)
    {
        var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = await gameShopContext.Users.FirstAsync(user => user.Id == idUser);
        var gameProduct = await gameProductRepository.GetGameProductAsync(id);

        var wishList = await gameShopContext.WishLists
            .Include(wishLists => wishLists.User)
            .Include(wishLists => wishLists.GameProduct)
            .Where(wishLists => wishLists.GameProduct.Id == id && wishLists.User.Id == idUser)
            .ExecuteDeleteAsync();

        return RedirectToAction("WishList", "Home");
    }
}
