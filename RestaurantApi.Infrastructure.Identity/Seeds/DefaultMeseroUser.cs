using Microsoft.AspNetCore.Identity;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Infrastructure.Identity.Entities;

namespace RestaurantApi.Infrastructure.Identity.Seeds
{
    public static class DefaultMeseroUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser meseroUser = new();
            meseroUser.UserName = "meserouser";
            meseroUser.Email = "meserouser@gmail.com";
            meseroUser.FirstName = "John";
            meseroUser.LastName = "Doe";
            meseroUser.EmailConfirmed = true;
            meseroUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != meseroUser.Id))
            {
                var user = await userManager.FindByEmailAsync(meseroUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(meseroUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(meseroUser, Roles.Mesero.ToString());
                }
            }


        }
    }
}
