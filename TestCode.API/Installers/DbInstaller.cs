using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCode.Data;
using TestCode.Domain;
using TestCode.Services;

namespace TestCode.API.Installers
{
    public class DbInstaller : IInstaler
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {

            services.AddDbContext<DataContext>(options =>
               options.UseInMemoryDatabase(databaseName: "mytestdatabase"));

            //services.AddDefaultIdentity<ApplicationUser>(options =>
            //{
            //    options.SignIn.RequireConfirmedAccount = true;
            //    options.Password.RequiredLength = 5;
            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredUniqueChars = 0;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //})
            //.AddRoles<IdentityRole>()
            //.AddEntityFrameworkStores<DataContext>();

            

            services.AddHttpContextAccessor();
            services.AddScoped<IPostService, PostService>();

        }
    }
}
