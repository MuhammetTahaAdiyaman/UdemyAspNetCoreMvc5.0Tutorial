using Microsoft.AspNetCore.Mvc;

namespace UdemyAspNetCore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
