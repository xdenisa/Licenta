using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Proiect.WebApp.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Proiect.WebApp.Code
{
    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddCurentUser(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor.HttpContext;
                var role = httpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                var admin = httpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
                return new LoginViewModel
                {
                    IsLogedIn = httpContext.User.Identity.IsAuthenticated,
                    IsPacient = role?.CompareTo("Patient")==0?bool.TrueString:bool.FalseString,
                    IsAdmin=admin!=null?bool.Parse(admin):false,
                    Id = httpContext.User.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value,
                    Email = httpContext.User.Claims?.FirstOrDefault(c => c.Type == "Email")?.Value
                };
            });

            return services;
        }
    }
}
