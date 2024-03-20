
using RestaurantApi.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApi.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using RestaurantApi.Infrastructure.Identity.Entities;
using RestaurantApi.Application.Interfaces.Services;
using RestaurantApi.Infrastructure.Identity.Services;
using RestaurantApi.Core.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestaurantApi.Core.Application.Dtos.Account;
using System.Text;

namespace RestaurantApi.Infrastructure.Identity
{
    public static class ServicesRegistration
	{
		public static void AddIdentityInfrasastructure(this IServiceCollection services, IConfiguration config)
		{
            #region "Contexts configuration"
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(option => option.UseInMemoryDatabase("IdentityDb")); 
            }
            else
            {
                var connectionString = config.GetConnectionString("IdentityConnection");
                services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString,
                    migrations => migrations.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            }
            #endregion

            #region "Identity"
            //ApplicationUser maneja los usuarios, IdentityRole maneja los roles
            services.AddIdentity<ApplicationUser, IdentityRole>()
                // esto ultimo permite que el mismo Identity genere el proceso de reseteo de pass o cambio correo
                // gestione internamente los tokens y la logica de relacionar ese usuario con ese token
             .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders(); 

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User";
                options.AccessDeniedPath = "/User/AccessDenied";
            });

            services.Configure<JWTSettings>(config.GetSection("JWTSettings")); // JWTSettings es el nombre que tiene la configuracion en appsettings.json

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = config["JWTSettings:Issuer"],
                    ValidAudience = config["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = c => // cuando manden un token no valido y que no tiene autorizacion
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not Authorized" });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, Error = "You are not Authorized to access this resource" });
                        return c.Response.WriteAsync(result);
                    }
                };
            });
            #endregion

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }

    }
}

 