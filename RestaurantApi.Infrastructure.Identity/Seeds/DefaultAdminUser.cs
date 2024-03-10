using Microsoft.AspNetCore.Identity;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Infrastructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultAdminUser = new();
            defaultAdminUser.UserName = "adminuser";
            defaultAdminUser.Email = "adminuser@gmail.com";
            defaultAdminUser.FirstName = "John";
            defaultAdminUser.LastName = "Doe";
            defaultAdminUser.EmailConfirmed = true;
            defaultAdminUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultAdminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultAdminUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdminUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultAdminUser, Roles.Mesero.ToString());
                    await userManager.AddToRoleAsync(defaultAdminUser, Roles.Admin.ToString());
                }
            }
        }
    }
}
