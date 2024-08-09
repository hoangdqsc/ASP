
using Application_Temp.Helpers;
using Core_Temp.Entities;
using Core_Temp.Interfaces;
using Infrastructure_Temp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Application_Temp.Extentions
{


public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            var passwordHelper = serviceProvider.GetRequiredService<IPasswordHelper>();


            // Kiểm tra xem có dữ liệu nào chưa, nếu có rồi thì không thêm
            if (await context.Users.AnyAsync())
        {
            return;
        }

        // Thêm dữ liệu mẫu cho Users
        context.Users.AddRange(
            new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = passwordHelper.HashPassword("Admin123!"),
                Roles = "Admin"
            },
            new User
            {
                Id = 2,
                Username = "user",
                Email = "user@example.com",
                PasswordHash = passwordHelper.HashPassword("User123!"),
                Roles = "User"
            }
        );

        await context.SaveChangesAsync();
    }
}

}
