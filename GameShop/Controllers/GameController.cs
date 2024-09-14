using GameShop.Repository.Interfaces;
using GameShop.ViewModel;
using GameShopModel.Data;
using GameShopModel.Entities;
using GameShopModel.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameShop.Controllers;

public class GameController(
    IGameProductRepository gameProductRepository
    , IRepositoryCart repositoryCart
    , GameShopContext gameShopContext
    , IHttpContextAccessor httpContextAccessor) : Controller
{
    public async Task<IActionResult> Details(int id)
    {
        var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        var gameProduct = await gameShopContext.GameProducts
            .Include(gameProduct => gameProduct.Genres)
            .Include(gameProduct => gameProduct.Images)
            .Include (gameProduct => gameProduct.Videos)
            .Include(gameProduct => gameProduct.MinimumSystemRequirements)
            .Include(gameProduct => gameProduct.RecommendedSystemRequirements)
            .FirstAsync(gameProduct => gameProduct.Id == id);

        var wishList = gameShopContext.WishLists
            .Include(wishList => wishList.User)
            .Include(wishList => wishList.GameProduct)
            .Where(wishList => wishList.User.Id == idUser && wishList.GameProduct.Id == id);

        var cart = gameShopContext.Carts
            .Include(wishList => wishList.User)
            .Include(wishList => wishList.GameProducts)
            .Where(wishList => wishList.User.Id == idUser && wishList.GameProducts.Contains(gameProduct));

        var wishGameProduct = new WishGameProduct
        {
            GameProduct = gameProduct,
        };
        if (wishList.Any())
        {
            wishGameProduct.ContainsWishGameProduct = true;
        }

        if(cart.Any())
        {
            wishGameProduct.ContainsGameProduct = true;
        }
        return View(wishGameProduct);
    }
    public async Task<IActionResult> Add(int id)
    {
        var gameProduct = await gameProductRepository.GetGameProductAsync(id);
        repositoryCart.Add(gameProduct);

        return Redirect("~/Cart/Index");
    }
    public async Task<IActionResult> AddWishList(int id)
    {
        var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = await gameShopContext.Users.FirstAsync(user => user.Id == idUser);
        
        var gameProduct = await gameShopContext.GameProducts
            .Include(gameProduct => gameProduct.Genres)
            .Include(gameProduct => gameProduct.Images)
            .Include(gameProduct => gameProduct.Videos)
            .Include(gameProduct => gameProduct.MinimumSystemRequirements)
            .Include(gameProduct => gameProduct.RecommendedSystemRequirements)
            .FirstAsync(gameProduct => gameProduct.Id == id);

        var newWishList = new WishList
        {
            GameProduct = gameProduct,
            User = user
        };

        var wishList = gameShopContext.WishLists
           .Include(wishList => wishList.User)
           .Include(wishList => wishList.GameProduct)
           .Where(wishList => wishList.User.Id == idUser && wishList.GameProduct.Id == id);
           
        var wishGameProduct = new WishGameProduct
        {
            GameProduct = gameProduct
        };

        wishGameProduct.ContainsWishGameProduct = true;

        var cart = gameShopContext.Carts
            .Include(wishList => wishList.User)
            .Include(wishList => wishList.GameProducts)
            .Where(wishList => wishList.User.Id == idUser && wishList.GameProducts.Contains(gameProduct));

        if (cart.Any())
        {
            wishGameProduct.ContainsGameProduct = true;
        }

        await gameShopContext.WishLists.AddAsync(newWishList);
        await gameShopContext.SaveChangesAsync();

        return View("Details", wishGameProduct);
    }
    public async Task<IActionResult> DeleteWishList(int id)
    {
        var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        await gameShopContext.WishLists
            .Include(wishList => wishList.User)
            .Include(wishList => wishList.GameProduct)
            .Where(wishList => wishList.User.Id == idUser && wishList.GameProduct.Id == id).ExecuteDeleteAsync();
        var gameProduct = await gameShopContext.GameProducts
            .Include(gameProduct => gameProduct.Genres)
            .Include(gameProduct => gameProduct.Images)
            .Include(gameProduct => gameProduct.Videos)
            .Include(gameProduct => gameProduct.MinimumSystemRequirements)
            .Include(gameProduct => gameProduct.RecommendedSystemRequirements)
            .FirstAsync(gameProduct => gameProduct.Id == id);

        var cart = gameShopContext.Carts
            .Include(wishList => wishList.User)
            .Include(wishList => wishList.GameProducts)
            .Where(wishList => wishList.User.Id == idUser && wishList.GameProducts.Contains(gameProduct));

        var wishGameProduct = new WishGameProduct
        {
            GameProduct = gameProduct,
        };

        if (cart.Any())
        {
            wishGameProduct.ContainsGameProduct = true;
        }

        return View("Details", wishGameProduct);
    }
}
