using Microsoft.AspNetCore.Identity;
using RestaurantApi.Core.Application.Enums;
using RestaurantApi.Infrastructure.Identity;
using RestaurantApi.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApi.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Mesero.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
         }
    }
}
