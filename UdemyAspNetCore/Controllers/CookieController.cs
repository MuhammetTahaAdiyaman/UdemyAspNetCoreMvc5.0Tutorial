using Microsoft.AspNetCore.Mvc;
using System;

namespace UdemyAspNetCore.Controllers
{
    public class CookieController : Controller
    {
        public IActionResult Index()
        {
            SetCookie();
            ViewBag.Cookie = GetCookie();
            return View();
        }

        private void SetCookie()
        {//cookie setlemek için httpcontext nesnemizin response methodu ile gerçekleştirilir
            //appen key ve value şeklinde çalışmaktadır
            HttpContext.Response.Cookies.Append("Course", "Asp Net Core", new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.Now.AddDays(10),
                HttpOnly = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
            });
        }
        //cookie get işlemi yapmak için httpcontext nesnemizin request metodunu kullanıyoruz.
        private string GetCookie()

        {
            string cookieValue = string.Empty;
            HttpContext.Request.Cookies.TryGetValue("Course", out cookieValue);
            return cookieValue;
        }
    }
}
