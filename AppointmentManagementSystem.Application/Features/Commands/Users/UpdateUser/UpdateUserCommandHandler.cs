using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Domain.Common;
using AppointmentManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResultDto<bool>>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
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
                user.UserRoles = request.RoleIds.Select(roleId => new UserRole
                {
                    UserId = user.Id,
                    RoleId = roleId
                }).ToList();

                await _context.SaveChangesAsync(cancellationToken);

                return ResultDto<bool>.Success(true,"Kullanıcı başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return ResultDto<bool>.Fail($"Kullanıcı güncellenirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
