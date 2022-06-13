using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class SeedRoles
    {
        public static async Task Seed(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            if(await roleManager.Roles.AnyAsync()) return; //Only executes if there is no roles in the database

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Operator"},
                new AppRole{Name = "Admin"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role); // Creates the roles
            }
            
            var user = new AppUser{UserName="admin"};
            
            await userManager.CreateAsync(user, "T3st3!"); // Creates a default user

            await userManager.AddToRolesAsync(user, new[] {"Admin", "Operator"}); // Adds the user to the Admin role
        }
    }
}