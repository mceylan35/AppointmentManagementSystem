using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Users.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserRoleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FindAsync(new object[] { request.UserId }, cancellationToken);

            if (user == null)
            {
                // throw new NotFoundException(nameof(User), request.UserId);
            }

           // user.Role = request.NewRole;
           var result= await _context.SaveChangesAsync(cancellationToken);

            return result>0;
        }
    }
}