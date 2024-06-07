using Microsoft.AspNetCore.Mvc;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Pizzas");
        }
    }
}
