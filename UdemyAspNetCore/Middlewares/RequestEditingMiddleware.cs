using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UdemyAspNetCore.Middlewares
{
    public class RequestEditingMiddleware
    {   //asp.net core da response ve request ile alakalı delegate olarak bir tane bulunuyor o da RequestDelegate
        private RequestDelegate _requestDelegate;

        //dependency injection
        public RequestEditingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        //middleware oluşturmak için bir tane metot eklememiz gerekmektedir.
        public async Task Invoke(HttpContext context)
            //request ve responselara HttpContext aracılığı ile ulaşabiliyorduk
        {   if (context.Request.Path.ToString() == "/yavuz") //request olarak ysk.com.tr/yavuz geldiği anda response olarak hoşgeldin yavuz olarak yaz
                await context.Response.WriteAsync("Hosgeldin Yavuz");
            else
                await _requestDelegate.Invoke(context); //delegeyi işaret eden metotları çalıştırmak için
        }
    }
}
