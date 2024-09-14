using GameShopModel.Data;
using GameShopModel.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GameShop.Controllers;

public class LibraryController(IHttpContextAccessor httpContextAccessor, GameShopContext gameShopContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }
        var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var gameProducts = new List<GameProduct>();

        var cart = gameShopContext.Carts
           .Include(cart => cart.User)
           .Include(cart => cart.GameProducts)
           .Where(cart => cart.User.Id == idUser);

        foreach (var product in cart)
        {
            gameProducts.AddRange(product.GameProducts);
        }

        return View(gameProducts);
    }
}
