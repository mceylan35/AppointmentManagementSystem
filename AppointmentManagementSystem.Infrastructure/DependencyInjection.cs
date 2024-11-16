using AppointmentManagementSystem.Infrastructure.Context;
using AppointmentManagementSystem.Infrastructure.Identity.Services.Abstract;
using AppointmentManagementSystem.Infrastructure.Identity.Services.Concrete;
using AppointmentManagementSystem.Infrastructure.Identity;
using AppointmentManagementSystem.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AppointmentManagementSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AppointmentManagementSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());
             
            services.AddScoped<IIdentityService, IdentityService>(); 
            services.AddScoped<ICurrentUser, CurrentUserService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                              .AddCookie(options =>
                              {
                                  options.LoginPath = "/Account/Login";
                                  options.LogoutPath = "/Account/Logout";
                                  options.AccessDeniedPath = "/Account/AccessDenied";
                                  options.ExpireTimeSpan = TimeSpan.FromHours(8); // Cookie süresi
                                  options.SlidingExpiration = true;
                                  options.Cookie.Name = "AppointmentSystem";
                              });
           services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("RequireUserRole", policy =>
                    policy.RequireRole("User"));
            });


            return services;
        }
    }
}