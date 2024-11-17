using AppointmentManagementSystem.Application.Common.Exceptions;
using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Application.DTOs.Users;
using AppointmentManagementSystem.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Queries.Users.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, ResultDto<UserDetailDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetUserDetailQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResultDto<UserDetailDto>> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.Id && u.IsActive, cancellationToken);

            if (user == null)
                throw new NotFoundException($"Kullanıcı bulunamadı: {request.Id}");

            var response= new UserDetailDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                IsActive = user.IsActive,
                RoleId = user.UserRoles.Select(ur => ur.RoleId).FirstOrDefault(),
                RoleName = user.UserRoles.Select(ur => ur.Role.Name).FirstOrDefault()
            };
            return ResultDto<UserDetailDto>.Success(response,"İşlem Başarılı");
        }
    }
}
