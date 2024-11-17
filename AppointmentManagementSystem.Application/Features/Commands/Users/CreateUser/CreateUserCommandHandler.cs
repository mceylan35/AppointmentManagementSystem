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
        IMediator _mediator;

        public CreateUserCommandHandler(IApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResultDto<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return ResultDto<bool>.Fail("Bu kullanıcı adı zaten kullanılıyor.");

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return ResultDto<bool>.Fail("Bu email adresi zaten kullanılıyor.");

            var entity = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
              //  Role = request.Role,
                IsActive = true
            };

            _context.Users.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
           await _mediator.Send(new UpdateUserRoleCommand { UserId=entity.Id, RoleId= request.RoleId});
            return ResultDto<bool>.Success(true, "Kullanıcı başarıyla eklendi.");
        }
    }
}