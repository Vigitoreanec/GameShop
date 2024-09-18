using GameShop.Extensions;
using GameShop.Repository;
using GameShop.ViewModel;
using GameShopModel.Data;
using GameShopModel.Entities;
using GameShopModel.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameShop.Controllers;
public enum Filter
{
    Ask,
    Desk
}
public class HomeController(
    GameShopContext gameShopContext, 
    IGameProductRepository gameProductRepository, 
    IHttpContextAccessor httpContextAccessor) : Controller
{
    private const int countPopularGames = 100;
    private const int minValueMonth = -1;
    
    
    public async Task<IActionResult> Index(Filter? selectFilter, string gameGenre, string nameSearchString)
    {
        var gameGenres = from g in gameShopContext.Genres
                         select g;

        var gameProducts = from g in gameShopContext.GameProducts
                           select g;

        if(!string.IsNullOrEmpty(nameSearchString) )
        {
            gameProducts = gameProducts.Where(
                gameProduct => gameProduct.Title.ToUpper().Contains(nameSearchString));
        }
        if (selectFilter is not null)
        {
            if (selectFilter == Filter.Ask)
            {
                gameProducts = gameProducts.OrderBy(gameProduct => gameProduct.Title);
            }
            else
            {
                gameProducts = gameProducts.OrderByDescending(gameProduct => gameProduct.Title);
            }
        }
        if (!string.IsNullOrEmpty(gameGenre))
        {
            gameProducts = gameProducts.Include(gameProduct => gameProduct.Genres)
                .Where(gameProduct => 
                                gameProduct.Genres.Where(genre => 
                                                    genre.Title.Contains(gameGenre)).Any());
        }

        var filtered = new FilteredGameProductViewModel
        {
            //Filter = new SelectList(new List<Filter> { Filter.Ask, Filter.Desk}),
            GameGenres = new SelectList(await gameGenres.Select(genre => genre.Title).ToListAsync()),
            GameProducts = await gameProducts.ToListAsync()
        };

        return View(filtered);
    }
    public async Task<IActionResult> PopularGames()
    {
        var carts = await gameShopContext.Carts
            .Include(cart => cart.GameProducts)
            .Where(cart => 
            cart.DatePurchese >= DateTime.UtcNow.Date.AddMonths(minValueMonth) &&
            cart.DatePurchese <= DateTime.UtcNow.Date)
            .ToListAsync();

        var dictionary = carts.SelectMany(cart => cart.GameProducts)
            .GroupBy(gameProduct => gameProduct.Id)
            .Take(countPopularGames)
            .ToDictionary( group => group.Key, group => group.First());

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
