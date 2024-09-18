using GameShop.Repository.Interfaces;
using GameShop.ViewModel;
using GameShopModel.Data;
using GameShopModel.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameShop.Controllers;

public class CartController(
    GameShopContext gameShopContext, 
    IRepositoryCart repositoryCart, 
    IHttpContextAccessor httpContextAccessor) : Controller
{
    public IActionResult Index()
    {
        var products = repositoryCart.GetProducts();
        var cartViewModel = new CartViewModel
        {
            GameProducts = products,
            SumGameProducts = repositoryCart.Sum
        };
        return View(cartViewModel);
    }
    public IActionResult Delete(int id)
    {
        repositoryCart.Delete(id);
        var products = repositoryCart.GetProducts();
        var cartViewModel = new CartViewModel
        {
            GameProducts = products,
            SumGameProducts = repositoryCart.Sum,
        };
        return View("Index", cartViewModel);
    }
    public async Task<IActionResult> PlaceOrder()
    {
        var idUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = gameShopContext.Users.Where(user =>  user.Id == idUser).First(); //---->
        var products = repositoryCart.GetProducts();
        var cart = new Cart
        {
            GameProducts = [],
            SumPurchese = repositoryCart.Sum,
            DatePurchese = DateTime.Now,
            User = user
        };
        foreach (var product in products)
        {
            var gameProduct = gameShopContext.GameProducts
                .Where(gameProduct => gameProduct.Id == product.Id)
                .First();
            cart.GameProducts.Add(gameProduct);
        }

        await gameShopContext.Carts.AddAsync(cart);
        await gameShopContext.SaveChangesAsync();

        repositoryCart.Clear();

        return RedirectToAction("Index", "Home");
    }
}
