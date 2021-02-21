using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypewrighterAPI.Data;
using TypewrighterAPI.Domain;
using TypewrighterAPI.Middleware;
using TypewrighterAPI.Services;

namespace TestCode.Installer
{
    public class DbInstaller : IInstaler
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {

            services.AddDbContext<DataContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
            })
            .AddPasswordValidator<UsernameAndPasswordValidator<ApplicationUser>>()
            .AddEntityFrameworkStores<DataContext>();  

            services.AddSingleton<IPostService, PostService>();
        }
    }
}
