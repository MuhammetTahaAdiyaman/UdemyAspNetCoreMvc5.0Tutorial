using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UdemyAspNetCore.Middlewares
{
    public class ResponseEditingMiddleware
    {
        private RequestDelegate _requestDelegate;

        public ResponseEditingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            await _requestDelegate.Invoke(context);
            if(context.Response.StatusCode == StatusCodes.Status404NotFound) //response olarak 404 not found döndüğü anda sayfa bulunamadı olarak yaz.
            {
                await context.Response.WriteAsync("Sayfa bulunamadi");
            }
        }
    }
}
