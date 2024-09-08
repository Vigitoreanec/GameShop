using GameShop.Repository.Interfaces;
using GameShopModel.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Controllers;

public class CartController(IGameProductRepository gameProductRepository, IRepositoryCart repositoryCart) : Controller
{
    public IActionResult Index()
    {
        var products = repositoryCart.GetProducts();
        return View(products);
    }
    public IActionResult Delete(int id)
    {
        repositoryCart.Delete(id);
        return View("Index", repositoryCart.GetProducts());
    }
}
