using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace TimeWare
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (context, next) => {
                int m1start = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
                await context.Response.WriteAsync("Middleware function (1) started... \r\n");
                await next();
                int m1end = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
                await context.Response.WriteAsync("Middleware function (1) time: " + (m1end - m1start).ToString() + "ms.\r\n");
            });
            app.Use(async (context, next) => {
                int m2start = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
                await context.Response.WriteAsync("Middleware function (2) started... \r\n");
                await next();
                int m2end = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
                await context.Response.WriteAsync("Middleware function (2) time: " + (m2end - m2start).ToString() + "ms.\r\n");
            });
            app.UseMiddleware<TimeMiddleWare>();
            app.Use(async (context, next) =>
            {
                int m3start = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
                await context.Response.WriteAsync("Middleware function (3) started... \r\n");
                int m3end = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
                await context.Response.WriteAsync("Middleware function (3) time: " + (m3end - m3start).ToString() + "ms.\r\n");
                //await context.Response.WriteAsync("Going down...");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
        }
    }
}
