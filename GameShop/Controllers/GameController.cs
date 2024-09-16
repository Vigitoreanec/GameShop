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
    , IRepositoryWishList repositoryWishList
    , GameShopContext gameShopContext
    , IHttpContextAccessor httpContextAccessor) : Controller
{
    public async Task<IActionResult> Details(int id)
    {
        if (httpContextAccessor.HttpContext is null || 
            httpContextAccessor.HttpContext.User is null ||
            httpContextAccessor.HttpContext.User.Identity is null)
        {
            return BadRequest();
        }

        var gameProduct = await gameShopContext.GameProducts
            .Include(gameProduct => gameProduct.Genres)
            .Include(gameProduct => gameProduct.Images)
            .Include(gameProduct => gameProduct.Videos)
            .Include(gameProduct => gameProduct.MinimumSystemRequirements)
            .Include(gameProduct => gameProduct.RecommendedSystemRequirements)
            .FirstAsync(gameProduct => gameProduct.Id == id);

        var wishGameProductVM = new WishGameProductViewModel
        {
            GameProduct = gameProduct,
        };

        if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            wishGameProductVM.ContainsGameProduct = 
                await gameShopContext.Carts
            .Include(wishList => wishList.User)
            .Include(wishList => wishList.GameProducts)
            .Where(wishList => wishList.User.Id == idUser && wishList.GameProducts.Contains(gameProduct))
            .AnyAsync();

            wishGameProductVM.ContainsWishGameProduct =
                await gameShopContext.WishLists
            .Include(wishList => wishList.User)
            .Include(wishList => wishList.GameProduct)
            .Where(wishList => wishList.User.Id == idUser && wishList.GameProduct.Id == id)
            .AnyAsync();
        }

        return View(wishGameProductVM);
    }
    public async Task<IActionResult> AddToCart(int id)
    {
        if (httpContextAccessor.HttpContext is null ||
           httpContextAccessor.HttpContext.User is null ||
           httpContextAccessor.HttpContext.User.Identity is null)
        {
            return BadRequest();
        }
        if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var gameProduct = await gameProductRepository.GetGameProductAsync(id);
        repositoryCart.Add(gameProduct);

        return RedirectToAction("Index", "Cart");
    }
    public async Task<IActionResult> Add(int id)
    {
        var gameProduct = await gameProductRepository.GetGameProductAsync(id);
        repositoryCart.Add(gameProduct);

        return RedirectToAction("Index", "Cart");
    }
    public async Task<IActionResult> AddWishList(int id)
    {
        if (httpContextAccessor.HttpContext is null ||
           httpContextAccessor.HttpContext.User is null ||
           httpContextAccessor.HttpContext.User.Identity is null)
        {
            return BadRequest();
        }

        if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = await gameShopContext.Users.FirstAsync(user => user.Id == idUser);

        var gameProduct = await gameProductRepository.GetGameProductAsync(id);

        await repositoryWishList.AddAsync(user,gameProduct);

        return RedirectToAction("Details", 
            new RouteValueDictionary(new { controller = "Game", action = "Details", Id = id }));
    }
    public async Task<IActionResult> DeleteWishList(int id)
    {
        if (httpContextAccessor.HttpContext is null ||
           httpContextAccessor.HttpContext.User is null ||
           httpContextAccessor.HttpContext.User.Identity is null)
        {
            return BadRequest();
        }

        var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        await repositoryWishList.DeleteAsync(id, idUser);

        return RedirectToAction("Details",
            new RouteValueDictionary(new 
            { 
                controller = "Game", 
                action = "Details", 
                Id = id 
            }));
    }
}
