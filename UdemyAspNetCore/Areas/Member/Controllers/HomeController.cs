using Microsoft.AspNetCore.Mvc;

namespace UdemyAspNetCore.Areas.Member.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
