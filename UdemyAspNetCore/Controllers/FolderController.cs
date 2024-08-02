using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace UdemyAspNetCore.Controllers
{
    public class FolderController : Controller
    {
        public IActionResult List()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot"));
            var folders = directoryInfo.GetDirectories();
            return View(folders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); //view'i biz form oluşturacağımız için get metodu içinde yapıyoruz
        }

        //form method post olduğu için save butonuna tıkladığı anda create'in post metodu çalışacak
        [HttpPost]
        public IActionResult Create(string folderName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName));
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            return RedirectToAction("List","Folder");
        }

        [HttpGet]
        public IActionResult Remove(string folderName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot",folderName));
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }
          
            return RedirectToAction("List","Folder");
        }
    }
}
