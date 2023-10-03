using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static void Initialize(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        SeedUsers(userManager);
        SeedRoles(roleManager);
    }

    public static void SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new IdentityRole { Name = "Admin" };

            IdentityResult roleResult = roleManager.CreateAsync(role).Result;
        }

        if (!roleManager.RoleExistsAsync("Client").Result)
        {
            IdentityRole role = new IdentityRole { Name = "Client" };

            IdentityResult roleResult = roleManager.CreateAsync(role).Result;
        }
    }

    public static void SeedUsers(UserManager<IdentityUser> userManager)
    {
        if (userManager.FindByNameAsync("admin@gmail.com").Result == null)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com"
            };

            IdentityResult result = userManager.CreateAsync(user, "Admin123@#").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }

        if (userManager.FindByNameAsync("ishan@gmail.com").Result == null)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = "ishan@gmail.com",
                Email = "ishan@gmail.com"
            };

            IdentityResult result = userManager.CreateAsync(user, "Ishan123@#").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Client").Wait();
            }
        }
    }
}
