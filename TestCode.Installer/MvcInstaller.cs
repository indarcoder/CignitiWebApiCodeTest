using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestCode.Installer
{
    public class MvcInstaller : IInstaler
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            var jwtSettings = new JwtSettings();
            Configuration.Bind(key: nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });


            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "TypewrighterAPI", Version = "v1" });

                x.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                });

                var security = new OpenApiSecurityRequirement();                
                security.Add(new OpenApiSecurityScheme() { Scheme = "Bearer" }, new string[0]);

                x.AddSecurityRequirement(security);
            });
        }
    }
}
