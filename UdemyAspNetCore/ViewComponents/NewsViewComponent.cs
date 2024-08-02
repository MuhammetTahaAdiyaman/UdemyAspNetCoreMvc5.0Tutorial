using Microsoft.AspNetCore.Mvc;
using UdemyAspNetCore.Models;

namespace UdemyAspNetCore.ViewComponents
{
    public class NewsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string color = "default")
        {
            var list = NewsContext.NewsList;
            if (color == "default")
            {
                return View(list);

            }
            else if (color == "red")
            {
                return View("red",list); //red.cshtml'e gidecek ve model list olarak dönecek
            }
            else
            {
                return View("blue", list);//eğer parametre olarak blue gelirse blue.cshtml açılacak ve list olarak dönecek.
            }
        }
    }
}
