using GameShop.Core;
using GameShop.Extensions;
using GameShop.Repository;
using GameShop.ViewModel;
using GameShopModel.Data;
using GameShopModel.Entities;
using GameShopModel.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;

namespace GameShop.Controllers;
public record NameSurname(string name, string surname);
public class HomeController(
    GameShopContext gameShopContext, 
    IGameProductRepository gameProductRepository, 
    IHttpContextAccessor httpContextAccessor) : Controller
{
    private const int countPopularGames = 100;
    private const int minValueMonth = -1;
    private const int pageSize = 4;

    public async Task<IActionResult> Index(
        //Filter? selectFilter, 
        string selectedGenreGameProduct, 
        string selectedTitleGameProduct,
        SortGameProductState? sortGameProductState,
        int page = 1
        )
    {
        var gameGenres = from g in gameShopContext.Genres
                         select g.Title;

        var gameProducts = from g in gameShopContext.GameProducts
                           select g;

        if(!string.IsNullOrEmpty(selectedTitleGameProduct) )
        {
            gameProducts = gameProducts
                .Where(gameProduct => gameProduct.Title.ToUpper()
                .Contains(selectedTitleGameProduct));
        }
        
        if (!string.IsNullOrEmpty(selectedGenreGameProduct))
        {
            gameProducts = gameProducts
                .Include(gameProduct => gameProduct.Genres)
                .Where(gameProduct => gameProduct.Genres
                .Any(genre => genre.Title.Contains(selectedGenreGameProduct)));
        }

        gameProducts = sortGameProductState switch
        {
            SortGameProductState.TitleAsk => gameProducts = gameProducts.OrderBy(gameProduct => gameProduct.Title),
            SortGameProductState.TitleDesk => gameProducts = gameProducts.OrderByDescending(gameProduct => gameProduct.Title),
            _ => gameProducts
        };

        var gameproductsVM = new GameProductsViewModel
        {
            GameProducts = await PaginationList<GameProduct>.CreateAsync(gameProducts, page, pageSize),
            //PageViewModel = new(count, page, pageSize),
            SortGameProductVM = new(sortGameProductState),
            FilteredGameProductVM = new(new(gameGenres), selectedGenreGameProduct, selectedTitleGameProduct)
        };

        return View(gameproductsVM);
    }
    public async Task<IActionResult> PopularGames(string searchTitle)
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

        //var games = from game in dictionary
        //           where game.Value.Title == dictionary.Values.Select(gameProduct => gameProduct.Title).First()
        //           select game;
        
        //if (!string.IsNullOrEmpty(searchTitle))
        //{
        //    games.Where(gameProduct => gameProduct.Value.Title.ToUpper().Contains(searchTitle.ToUpper()));
        //}
        return View(dictionary);
    }
   
    
    public async Task<IActionResult> RecommendationGames() // Эксперты
    {
        //var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        
        var recommendedGameProducts = await gameShopContext.RecommendedGameProducts
            .Include(recommendedGameProducts => recommendedGameProducts.GameProduct)
            .ToListAsync();

        var listGameProductByExpert = new Dictionary<NameSurname, List<GameProduct>>();
        foreach(var product in recommendedGameProducts)
        {
            var nameSurname = new NameSurname(product.ExpertName, product.ExpertSurname);
            if (!listGameProductByExpert.TryGetValue(nameSurname, out List<GameProduct>? value))
            {
                listGameProductByExpert.Add(nameSurname, [product.GameProduct]);
            }
            else
            {
                value.Add(product.GameProduct);
            }
        }
        return View(listGameProductByExpert);
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
        var gameProduct = await gameProductRepository.GetByIdAsync(id);

        var wishList = await gameShopContext.WishLists
            .Include(wishLists => wishLists.User)
            .Include(wishLists => wishLists.GameProduct)
            .Where(wishLists => wishLists.GameProduct.Id == id && wishLists.User.Id == idUser)
            .ExecuteDeleteAsync();

        return RedirectToAction("WishList", "Home");
    }
}
