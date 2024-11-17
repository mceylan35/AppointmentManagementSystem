using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Application.Features.Commands.Users.UpdateUserRole;
using AppointmentManagementSystem.Domain.Common;
using AppointmentManagementSystem.Domain.Entities;
using BCrypt.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResultDto<bool>>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateUserAsync(request, cancellationToken);
            if (!validationResult.Successed)
                return validationResult;

            var user = CreateUser(request);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await AssignUserRoleAsync(user, request.RoleId, cancellationToken);


            return ResultDto<bool>.Success(true, "Kullanıcı başarıyla eklendi.");
        }
        private async Task<ResultDto<bool>> ValidateUserAsync(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username, cancellationToken))
                return ResultDto<bool>.Fail("Bu kullanıcı adı zaten kullanılıyor.");

            if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
                return ResultDto<bool>.Fail("Bu email adresi zaten kullanılıyor.");

            if (!await _context.Roles.AnyAsync(r => r.Id == request.RoleId, cancellationToken))
                return ResultDto<bool>.Fail("Geçersiz rol ID'si.");

            return ResultDto<bool>.Success(true);
        }
        private static User CreateUser(CreateUserCommand request)
        {
            return new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UserRoles = new List<UserRole>() 
            };
        }
        private async Task AssignUserRoleAsync(User user, Guid roleId, CancellationToken cancellationToken)
        {
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = roleId
            };
             
            await _context.UserRoles.AddAsync(userRole, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}