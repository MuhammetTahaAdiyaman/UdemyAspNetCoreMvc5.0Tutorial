using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace UdemyAspNetCore.Controllers
{
    public class FileController : Controller
    {
        public IActionResult List()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files"));
            var files = directoryInfo.GetFiles();
            return View(files);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //kullanıcı create.cshtml içinden save butonuna tıklayınca post metodu çalışacak
        [HttpPost]
        public IActionResult Create(string fileName)
        {
            FileInfo fileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","files",fileName));
            if (!fileInfo.Exists )
            {
                fileInfo.Create();
            }
            return RedirectToAction("List","File");
        }

        //streamwriter ile .txt dosyası oluşturalım.
        public IActionResult CreateWithData()
        {
            FileInfo fileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", Guid.NewGuid().ToString() + ".txt"));
            StreamWriter streamWriter = fileInfo.CreateText();
            streamWriter.Write("Merhaba ben Taha");
            streamWriter.Close();
            return RedirectToAction("List","File");
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile formFile)
        {
            //bir de şunu yapabiliriz dosyayı filtreleme yani sadece jpeg veya png olarak filtreleyebiliriz.
            if(formFile.ContentType == "image/png")
            {
                //şimdi aynı dosya ismine sahip iki dosya gelirse kaydetmemesi için biz öncelikle Guid kullanacağız ancak dosyanın uzantısınıda birlikte alalım
                //dosya uzantısını alabilmek için
                var extension = Path.GetExtension(formFile.FileName);
                var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/images/" + Guid.NewGuid() + extension;
                FileStream fileStream = new FileStream(path, FileMode.Create); //bizden bir path ve bir file mode istiyor
                formFile.CopyTo(fileStream); //bu bizden bir stream dosyası istiyor

                TempData["message"] = "Dosya upload işlemi başarılı şekilde gerçekleşti";
            }
            else
            {
                TempData["message"] = "Dosya upload işlemi başarısız. Lütfen dosya türünüzü png olarak yükleyiniz!";

            }
            return RedirectToAction("Upload", "File");
        }

        [HttpGet]
        public IActionResult Remove(string fileName)
        {
            FileInfo fileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileName));
            if(fileInfo.Exists)
            {
                fileInfo.Delete();  
            }
            return RedirectToAction("List", "File");
        }
    }
}
