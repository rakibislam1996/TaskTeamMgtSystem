using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Infrastructure
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(TaskTeamMgtSystemDbContext db)
        {
            if (!await db.Users.AnyAsync())
            {
                var passwordHasher = new PasswordHasher<User>();

                var admin = new User
                {
                    FullName = "Admin User",
                    Email = "admin@demo.com",
                    Role = "Admin"
                };
                admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123!");

                var manager = new User
                {
                    FullName = "Manager User",
                    Email = "manager@demo.com",
                    Role = "Manager"
                };
                manager.PasswordHash = passwordHasher.HashPassword(manager, "Manager123!");

                var employee = new User
                {
                    FullName = "Employee User",
                    Email = "employee@demo.com",
                    Role = "Employee"
                };
                employee.PasswordHash = passwordHasher.HashPassword(employee, "Employee123!");

                db.Users.Add(admin);
                db.Users.Add(manager);
                db.Users.Add(employee);
                await db.SaveChangesAsync();
            }
        }
    }
}
