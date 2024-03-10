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
    public static class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultSuperAdminUser = new();
            defaultSuperAdminUser.UserName = "superadminuser";
            defaultSuperAdminUser.Email = "superadminuser@email.com";
            defaultSuperAdminUser.FirstName = "John";
            defaultSuperAdminUser.LastName = "Doe";
            defaultSuperAdminUser.EmailConfirmed = true;
            defaultSuperAdminUser.PhoneNumberConfirmed = true;

            if(userManager.Users.All(u=> u.Id != defaultSuperAdminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultSuperAdminUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultSuperAdminUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultSuperAdminUser, Roles.Mesero.ToString());
                    await userManager.AddToRoleAsync(defaultSuperAdminUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultSuperAdminUser, Roles.SuperAdmin.ToString());
                }
            }
         
        }
    }
}
