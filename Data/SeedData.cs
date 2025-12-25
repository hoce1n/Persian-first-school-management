using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;

public static class SeedData
{
    public static async Task SeedAdminAsync(IServiceProvider service)
    {
        var dbContext = service.GetRequiredService<ApplicationDbContext>();

        var adminUsername = "Manager";
        var adminEmail = "1hccein@gmail.com";
        var adminPhone = "09039328727";
        var adminPassword = "Admin@123";

        var admin = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == adminUsername);
        if (admin == null)
        {
            admin = new User
            {
                UserName = adminUsername,
                PhoneNumber = adminPhone,
                Role = Role.Admin
            };

            var hasher = new PasswordHasher<User>();
            admin.PasswordHash = hasher.HashPassword(admin, adminPassword);

            dbContext.Users.Add(admin);
            await dbContext.SaveChangesAsync();
        }
    }
}