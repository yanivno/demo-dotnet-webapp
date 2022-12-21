using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace aspnet
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.MapWhen(
              context => {
                var path = context.Request.Path.Value.ToLower();
                return
                    path.StartsWith("/assets") ||
                    path.StartsWith("/lib") ||
                    path.StartsWith("/app");
                },
              config => config.UseStaticFiles());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                  Console.WriteLine("Displaying page");
                    await context.Response.WriteAsync(
                      @"<!DOCTYPE html>
                      <html>
                        <head>
                          <title>Powered By Tanzu Buildpacks Yaniv</title>
                        </head>
                        <body>
                          Hi From Tanzu
                          <img style=""display: block; margin-left: auto; margin-right: auto; width: 50%;"" src=""/assets/logo-vmware-tanzu-square-Header.png""></img>
                        </body>
                      </html>"
                      );
                });
            });
        }
    }
}
