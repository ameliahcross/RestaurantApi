
using RestaurantApi.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApi.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using RestaurantApi.Infrastructure.Identity.Entities;
using RestaurantApi.Application.Interfaces.Services;
using RestaurantApi.Infrastructure.Identity.Services;

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

            services.AddAuthentication();
            #endregion

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }

    }
}

 