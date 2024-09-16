using GameShop.Extensions;
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
    private const int countPopularGames = 100;
    public async Task <IActionResult> Index()
    {
        var gameProducts = await gameProductRepository.GetAllGameProductsAsync();
        return View(gameProducts);
    }
    //public async Task<IActionResult> Index(string searchString)
    //{
    //    var gameProducts = await gameProductRepository.GetAllGameProductsAsync();
    //    gameProducts.Where(gameProduct => 
    //    gameProduct.Title.ToUpper()
    //                .Contains(searchString.ToUpper()))
    //                .ToList();

    //    return View(gameProducts);
    //}
    public async Task<IActionResult> PopularGames()
    {
        var carrentDate = DateTime.UtcNow;
        var monthAgo = new DateTime(carrentDate.Year, carrentDate.Month - 1, carrentDate.Day);

        var games = await gameShopContext.Carts
            .Include(cart => cart.GameProducts)
            .Between(cart => cart.DatePurchese, monthAgo, carrentDate)
            .ToListAsync();

        var dictionary = new Dictionary<int, GameProduct>();

        foreach (var cart in games)
        {
            if (dictionary.Count > countPopularGames)
            {
                break;
            }

            foreach (var product in cart.GameProducts)
            {
                if (dictionary.ContainsKey(product.Id))
                {
                    continue;
                }
                dictionary.Add(product.Id, product);
            }
        }

        return View(dictionary);
    }
    public IActionResult RecommendationGames() // Эксперты
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
