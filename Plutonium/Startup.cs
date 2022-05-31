using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Plutonium.BackgroundServices;
using Plutonium.Helpers;
using Plutonium.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plutonium
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
            services.AddControllersWithViews();
            services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddSignalR();
            //   services.AddHostedService<TimedHostedService>();
            services.AddSingleton<ProcessBackgroundService>();
            services.AddHostedService<BackgroundServiceStarter<ProcessBackgroundService>>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            logger.AddLog4Net("log4net.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<ProcessHub>("/processHub");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ProcessHub>("/hubs/processHub");
                // other endpoints
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
             "Crud",
             "Crud/Index/{modelName}",
             new { controller = "Crud", action = "Index" });



                endpoints.MapControllerRoute(
             "Crud",
             "Crud/GetItems/{modelName}",
             new { controller = "Crud", action = "GetItems" });


                //   endpoints.MapControllerRoute(
                //"Crud",
                //"Crud/Create/{modelName}/{o}",
                //new { controller = "Crud", action = "Create" });

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
