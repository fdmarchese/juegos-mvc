using Microsoft.AspNetCore.Mvc;

namespace juegos_mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
