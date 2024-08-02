using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using UdemyAspNetCore.Models;

namespace UdemyAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() //Bu action method Views klasörü içinde Home klasörü altında Index.cshtml yönetir
        {
            //ViewBag.Name = "Taha";
            //ViewData["Name"] = "Yasemen";

            ////tempdata ile action methodlar arası veri taşıma yapılabilir
            //TempData["Name"] = "Hüseyin";

            ////model ile veri taşıma işlemi için bir modele ihtiyacımız bulunmaktadır.
            ////modeli oluşturduktan sonra burada bir instance alalım
            //Customer customer = new() {Age = 26, FirstName="Taha",LastName="Adıyaman" };
            //return View(customer);

            var customers = CustomerContext.Customer;
            return View(customers);
        }

        /*
        public IActionResult Yavuz() //Bu action method Views klasörü içinde Home klasörü altında Yavuz.cshtml yönetir
        {   //eğer biz view'a string bir değer gönderirsek artık Views klasörü altında Home içinde Yavuz.cshtml'i değil gönderilen değer varsa .cshtml dosyası olarak onu çalıştırır
            //iki yerde arama yapar bir Views klasörü içinde bir de Shared klasörü içinde
            //aynı zamanda model de gönderebiliriz
            Customer customer1 = new() { Age = 26, FirstName = "Taha", LastName = "Adıyaman" };

            return View("Taha",customer1);

        }
        */
        [HttpGet]
        public IActionResult Create()
        {
            //asp-for kullanmak için
            return View(new Customer());
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            //Formda bulunan nesneleri ilk başta şu şekilde yakalayacağız ileride model binding geldiğinde daha kolay şekilde yapabileceğiz.
            //var firstName = HttpContext.Request.Form["firstName"].ToString();
            //var lastName = HttpContext.Request.Form["lastName"].ToString();
            //var age = int.Parse(HttpContext.Request.Form["age"].ToString());

            //şimdi eklediğimiz değerlerin idsini otomatik olarak artmasını istiyoruz bundan dolayı son elemana ulaşıp idsini+1 yapacağız
            //son durum güncellemesi: şimdi kontrol sağlayarak son kullanıcıya ulaşmamız gerek listede hiç eleman yoksa eleman eklediğimizde son kullanıcı olmadığı için kod patlayacak
            //var lastCustomer = CustomerContext.Customer.Last();
            //var id = lastCustomer.Id + 1;
            //patlamayan kod:
            ModelState.Remove("id");
            if (ModelState.IsValid)
            {
                Customer lastCustomer = null;
                if (CustomerContext.Customer.Count > 0)
                {
                    lastCustomer = CustomerContext.Customer.Last();
                }
                customer.Id = 1;
                if (lastCustomer != null)
                {
                    customer.Id = lastCustomer.Id + 1;
                }

                //şimdi listemize yeni elemanı eklemek için kodlarımızı yazalım.
                CustomerContext.Customer.Add(customer);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            //biz silme işlemini idlere göre yapacağız
            //öncelikle routeda yer alan id ile customer nesnesinde yer alan id kıyaslaması eşit mi onu yapalım eşit ise true ise sildirme işlemini gerçekleştirelim.
            //neden true çünkü kullanacağımız Find metodu predicate delege olduğundan dolayı predicate delege bir veri türü alır ve bool değer döner

            //route yapısındaki id değerini yakalamak için;
            //var id = int.Parse(RouteData.Values["id"].ToString());

            //müşteri nesnesindeki id değeri ile yukarıdaki id değeri eşit mi onu yazalım.
            var removeCustomer = CustomerContext.Customer.Find(I => I.Id == id);
            CustomerContext.Customer.Remove(removeCustomer);

            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            //şimdi ilk olarak yapmamız gereken şey view olarak sayfayı home içine oluşturalım.
            //route id'yi bulalım.
            //var id = int.Parse(RouteData.Values["id"].ToString());
            //route id ile customer nesnesindeki id eşit olanları getirmesi için bir kod yazalım
            var updatedCustomer = CustomerContext.Customer.FirstOrDefault(I=> I.Id == id);
            //ve bunu model olarak yollayalım
            //update sayfasına gidelim ve orada modelimiz üzerinden form nesnelerinin değerlerini dolu olarak gelmesini sağlayalım.
            return View(updatedCustomer);
        }

        [HttpPost]
        public IActionResult Update(Customer customer)
        { //ilk olarak customerı yakalamamız gerekiyor id üzerinden. Formda olan nesneyi httpcontext.request.form ile değerini yakalayabiliyorduk
            //var id = int.Parse(HttpContext.Request.Form["id"].ToString());
            //formda bulunan id değerini yakaladık peki bu değer ile customerda bulunan id değeri aynı mı bir bakalım
            var updateCustomer = CustomerContext.Customer.FirstOrDefault(i => i.Id == customer.Id);
            //aynı olan değerleri updateCustomer üzerinden erişebileceğiz.
            //şimdi alanların değişikliklerini set edelim.
            //updateCustomer.FirstName = HttpContext.Request.Form["firstName"].ToString();
            //updateCustomer.LastName = HttpContext.Request.Form["lastName"].ToString();
            //updateCustomer.Age = int.Parse(HttpContext.Request.Form["age"].ToString());

            updateCustomer.FirstName = customer.FirstName;
            updateCustomer.LastName = customer.LastName;
            updateCustomer.Age = customer.Age;

            return RedirectToAction("Index", "Home");


        }

        //public IActionResult Status(int? code)
        //{
        //    return View();
        //}

        public IActionResult Error()
        {      //hatanın nerede olduğuna ve ne olduğuna eriştiğimiz yer burası
               var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //kendi log dosyamızı oluşturalım hataları tutacak olan
            var logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
            var logFileName = DateTime.Now.ToString();
            logFileName = logFileName.Replace(" ", "_");
            logFileName = logFileName.Replace(":", "-");
            logFileName = logFileName.Replace("/", "-");
            logFileName += ".txt";
            var logFilePath = Path.Combine(logFolderPath,logFileName);

            DirectoryInfo directoryInfo = new DirectoryInfo(logFolderPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            FileInfo fileInfo = new FileInfo(logFilePath);  
           
            var writer = fileInfo.CreateText();
            writer.WriteLine("Hatanın Gerçekleştiği Yer: " + exceptionHandlerPathFeature.Path);
            writer.WriteLine("Hata Mesajı: " + exceptionHandlerPathFeature.Error.Message);
            writer.Close();
            return View();
        }

        public IActionResult Hata()
        {
            throw new System.Exception("Sistemsel bir hata oluştu");
        }
    }
}
