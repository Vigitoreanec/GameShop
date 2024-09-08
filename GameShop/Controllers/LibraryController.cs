using Microsoft.AspNetCore.Mvc;

namespace GameShop.Controllers;

public class LibraryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
