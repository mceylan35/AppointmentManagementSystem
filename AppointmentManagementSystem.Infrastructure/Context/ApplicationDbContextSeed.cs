using AppointmentManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Infrastructure.Context
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultRolesAsync(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
            {
                new Role
                {
                    Id=Guid.NewGuid(),
                    Name = "Admin",
                    Description = "Sistem yöneticisi",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    IsDeleted=false
                },
                new Role
                {
                    Id=Guid.NewGuid(),
                    Name = "User",
                    Description = "Normal kullanıcı",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    IsDeleted=false
                }
            };

                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedDefaultUsersAsync(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
                var userRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "User");

                var adminUser = new User
                {
                    Id=Guid.NewGuid(),
                    Username = "admin",
                    Email = "admin@system.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    IsDeleted = false

                };

                var defaultUser = new User
                {
                    Id = Guid.NewGuid(),
                    Username = "user",
                    Email = "user@system.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    IsDeleted = false
                };

                context.Users.AddRange(adminUser, defaultUser);
                await context.SaveChangesAsync();

                var userRoles = new List<UserRole>
            {
                new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id },
                new UserRole { UserId = defaultUser.Id, RoleId = userRole.Id }
            };

                context.UserRoles.AddRange(userRoles);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedDefaultServicesAsync(ApplicationDbContext context)
        {
            if (!context.Services.Any())
            {
                var services = new List<Service>
            {
                new Service
                {
                    Id=Guid.NewGuid(),
                    Name = "Egzoz Gazı Ölçümü",
                    Description = "Araç egzoz emisyon ölçümü",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    IsDeleted=false
                },
                new Service
                {
                    Id=Guid.NewGuid(),
                    Name = "Fren Testi",
                    Description = "Araç fren sistemi kontrolü",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    IsDeleted=false
                },
                new Service
                {
                    Id=Guid.NewGuid(),
                    Name = "Far Ayarı",
                    Description = "Araç far ayar kontrolü",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    IsDeleted=false
                }
            };

                context.Services.AddRange(services);
                await context.SaveChangesAsync();
            }
        }
    }
}
