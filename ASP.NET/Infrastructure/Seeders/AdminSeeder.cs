using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Data.Entities;

namespace Infrastructure.Seeders;

public static class AdminSeeder
{
  public static async Task SeedAdminUser(IServiceProvider services)
  {
    var userManager = services.GetRequiredService<UserManager<UserEntity>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    var email = "admin@admin.com";
    var adminUser = await userManager.FindByEmailAsync(email);

    var roles = new[] { "Admin", "User" };
    foreach (var role in roles)
    {
      if (!await roleManager.RoleExistsAsync(role))
      {
        await roleManager.CreateAsync(new IdentityRole(role));
      }
    }

    if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
      await userManager.AddToRoleAsync(adminUser, "Admin");
    }
  }
}
