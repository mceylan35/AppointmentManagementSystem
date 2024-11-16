using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultDto<bool>>
    {
        private readonly IApplicationDbContext _context;

        public DeleteUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

                if (user == null)
                    return ResultDto<bool>.Fail("Kullanıcı bulunamadı.");

                // Soft delete
                user.IsDeleted = true;
                user.IsActive = false;
                user.LastModifiedAt = DateTime.UtcNow;
               
                await _context.SaveChangesAsync(cancellationToken);

                return ResultDto<bool>.Success(true,"Kullanıcı başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return ResultDto<bool>.Fail($"Kullanıcı silinirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
