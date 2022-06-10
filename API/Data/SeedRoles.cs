using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class SeedRoles
    {
        public static async Task Seed(RoleManager<AppRole> roleManager)
        {
            if(await roleManager.Roles.AnyAsync()) return; //Only executes if there is no roles in the database

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Operator"},
                new AppRole{Name = "Admin"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}