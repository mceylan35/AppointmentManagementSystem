using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Application.DTOs;
using AppointmentManagementSystem.Domain.Entities;
using AppointmentManagementSystem.Infrastructure.Identity.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text; 

namespace AppointmentManagementSystem.Infrastructure.Identity.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly IApplicationDbContext _context;

        public IdentityService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.Username == username);

                if (user == null)
                {
                    return new AuthenticationResult
                    {
                        Success = false,
                        Message = "Kullanıcı bulunamadı."
                    };
                }

                if (!VerifyPasswordHash(password, user.PasswordHash))
                {
                    return new AuthenticationResult
                    {
                        Success = false,
                        Message = "Hatalı şifre."
                    };
                }

                if (user.IsDeleted)
                {
                    return new AuthenticationResult
                    {
                        Success = false,
                        Message = "Hesabınız silinmiştir."
                    };
                }
                if (!user.IsActive)
                {
                    return new AuthenticationResult
                    {
                        Success = false,
                        Message = "Hesabınız aktif değildir."
                    };
                }

                // Kullanıcının rollerini al
                var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

                // Claims oluştur
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                // Rolleri claims'e ekle
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                return new AuthenticationResult
                {
                    Success = true,
                    Message = "Giriş başarılı.",

                    //User = user,
                    //Roles = roles,
                     Claims = claims
                };
            }
            catch (Exception ex)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    Message = $"Giriş işlemi sırasında bir hata oluştu: {ex.Message}"
                };
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }

        public async Task<string> CreateUserAsync(User user, string password, string role = "User")
        {
            try
            { 
                if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                    return "Bu kullanıcı adı zaten kullanılıyor.";

                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                    return "Bu email adresi zaten kullanılıyor.";
                 
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                 
                var userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == role);
                if (userRole == null)
                    return "Belirtilen rol bulunamadı.";
                 
                user.UserRoles.Add( new UserRole { Role = userRole } );
                 
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return string.Empty; // Başarılı
            }
            catch (Exception ex)
            {
                return $"Kullanıcı oluşturulurken bir hata oluştu: {ex.Message}";
            }
        }

        public async Task<bool> IsInRoleAsync(Guid userId, string role)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserRoles.Any(ur => ur.Role.Name == role) ?? false;
        }

        public async Task<List<string>> GetUserRolesAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserRoles.Select(ur => ur.Role.Name).ToList() ?? new List<string>();
        }
    }
}