using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UdemyAspNetCore.Middlewares;

namespace UdemyAspNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler("/Home/Error"); //bir hata ile karþýlaþýldýðýnda buna git diyoruz.
            //app.UseStatusCodePagesWithReExecute("/Home/Status", "?code={0}");

            app.UseStaticFiles(); //normal eriþime kapalý olan dosyalarý eriþime açmak için kullanýyoruz
            //node_modules klasörünü eriþime açmak için kullandýðýmýz yöntem
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules"))
            });
            //mvc tasarým mimarisini eklemek için
            app.UseRouting();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"area",
                    pattern:"{Area}/{Controller=Home}/{Action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Controller}/{Action}/{id?}",
                    defaults: new{Controller = "Home", Action = "Index" });
            });
            /*middleware için kullandýk

            app.UseMiddleware<ResponseEditingMiddleware>();
            app.UseMiddleware<RequestEditingMiddleware>();

            */

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
