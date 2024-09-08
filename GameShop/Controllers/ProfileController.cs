using Microsoft.AspNetCore.Mvc;

namespace GameShop.Controllers;

public class ProfileController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
