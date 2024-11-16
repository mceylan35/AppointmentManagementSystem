using AppointmentManagementSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<ResultDto<bool>>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Guid> RoleIds { get; set; }
        public bool IsActive { get; set; }
    }
}
