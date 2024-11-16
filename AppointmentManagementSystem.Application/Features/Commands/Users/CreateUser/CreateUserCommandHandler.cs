using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Domain.Common;
using AppointmentManagementSystem.Domain.Entities;
using BCrypt.Net;
using MediatR;
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

            return ResultDto<bool>.Success(true, "Kullanıcı başarıyla eklendi.");
        }
    }
}