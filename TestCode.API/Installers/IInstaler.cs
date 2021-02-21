using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCode.API.Installers
{
    public interface IInstaler 
    {
        void InstallServices(IServiceCollection services ,IConfiguration Configuration);
    }
}
