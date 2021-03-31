using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Proiect.BusinessLogic;
using Proiect.DataAccess;
using Proiect.DataAccess.EntityFramework;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proiect.WebApp.Code;
using Proiect.WebApp.Models;
using Proiect.WebApp.Models.Account;

namespace Proiect.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddControllersWithViews();
            services.AddSession();
            services.AddRazorPages();
            services.AddScoped<ProjectContext>();
            services.AddScoped<UnitOfWork>();
            services.AddScoped<UserAccountService>();
            services.AddScoped<SpecializationService>();
            services.AddScoped<MedicService>();
            services.AddScoped<ImageService>();
            services.AddScoped<AppointmentService>();
            services.AddScoped<PatientService>();
            services.AddScoped<PortfolioService>();
            services.AddScoped<MedicineService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<LoginViewModel>();
            services.AddCurentUser();

            services.AddAuthentication("ProiectCookies")
                  .AddCookie("ProiectCookies", options =>
                  {
                      options.AccessDeniedPath = new PathString("/Login/Index");
                      options.LoginPath = new PathString("/Login/Index");
                  });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Shared/ErrorPage");
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
