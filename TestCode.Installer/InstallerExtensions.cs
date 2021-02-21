using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCode.Installer
{
    public static class InstallerExtensions 
    {
        public static void InstallServicesInAssembly(this IServiceCollection services,IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
            typeof(Installers.IInstaler).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<Installers.IInstaler>().ToList();

            installers.ForEach(IInstaler => IInstaler.InstallServices(services, configuration));
        }     
    }
   
}
