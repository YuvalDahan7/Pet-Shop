using Microsoft.AspNetCore.Mvc;

namespace PeitsShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public HomeController()
        {
            
        }
    }
}
