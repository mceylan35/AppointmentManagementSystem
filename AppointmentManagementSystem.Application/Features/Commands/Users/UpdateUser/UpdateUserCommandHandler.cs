using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Domain.Common;
using AppointmentManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResultDto<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateUserCommandHandler(IApplicationDbContext context, ICurrentUser currentUser, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _currentUser = currentUser;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResultDto<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.UserRoles)
                    .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

                if (user == null)
                    return ResultDto<bool>.Fail("Kullanıcı bulunamadı.");

                // Username ve Email benzersizlik kontrolü
                if (await _context.Users.AnyAsync(u => u.Username == request.Username && u.Id != request.Id))
                    return ResultDto<bool>.Fail("Bu kullanıcı adı zaten kullanılıyor.");

                if (await _context.Users.AnyAsync(u => u.Email == request.Email && u.Id != request.Id))
                    return ResultDto<bool>.Fail("Bu email adresi zaten kullanılıyor.");


                user.Username = request.Username;
                user.Email = request.Email;
                user.IsActive = request.IsActive;

                // Rolleri güncelle
                _context.UserRoles.RemoveRange(user.UserRoles);
                user.UserRoles.Add(new UserRole
                {
                    UserId = user.Id,
                    RoleId = request.RoleId
                });

                 
                await _context.SaveChangesAsync(cancellationToken);



                if (_currentUser.Id == user.Id)
                {
                    var principal = await CreateNewClaimsPrincipal(user);
                    await _httpContextAccessor.HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal);

                }


                return ResultDto<bool>.Success(true,"Kullanıcı başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return ResultDto<bool>.Fail($"Kullanıcı güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        private async Task<ClaimsPrincipal> CreateNewClaimsPrincipal(User user)
        {
            var roles = await _context.Roles
                .Where(r => user.UserRoles.Select(ur => ur.RoleId).Contains(r.Id))
                .Select(r => r.Name)
                .ToListAsync();

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

            // Rolleri ekle
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}
