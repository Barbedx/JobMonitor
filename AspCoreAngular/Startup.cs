using AspCoreAngular.HubConfig;
using AspCoreAngular.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace AspCoreAngular
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
            services.AddLogging(b => b.AddConsole().AddDebug().AddEventSourceLogger());
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddCors(o => o.AddPolicy("CorsPolicy", p =>
           {
               p.
               AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin();
           }));
            services.AddSignalR(cnfg => cnfg.EnableDetailedErrors=true);
            services.AddDbContext<SqlJobMonitorContext>(optionsBuilder =>
            {
                optionsBuilder.UseLoggerFactory(services.BuildServiceProvider().GetService<ILoggerFactory>());
                optionsBuilder.UseSqlServer(Configuration["connectionStrings:SqlAzureDatabase"]);

            }
            );
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSpaStaticFiles();


            app.UseCors("CorsPolicy");
            app.UseSignalR(r =>
            {
                r.MapHub<MessageHub>("/hub");
                r.MapHub<JobHub>("/jobhub");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

        }
    }
}
