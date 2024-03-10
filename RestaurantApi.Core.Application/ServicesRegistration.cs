using Microsoft.Extensions.DependencyInjection;
using RestaurantApi.Core.Application.Helpers;
using RestaurantApi.Core.Application.Interfaces.Services;
using RestaurantApi.Core.Application.Services;
using System.Reflection;

namespace RestaurantApi.Core.Application
{
    public static class ServicesRegistration
	{
		public static void AddApplicationLayer(this IServiceCollection services)
		{
            #region "Services"
            services.AddTransient<IUserService, UserService>();
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));

            #endregion

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

    }
}

 